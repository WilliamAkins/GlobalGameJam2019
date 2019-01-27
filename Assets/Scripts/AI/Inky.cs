using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inky : MonoBehaviour
{

    [SerializeField]
    Vector3 baseOfOperations;

    GameManager gm;

    NavMeshAgent agent;


    Transform player;

    Animator anim;
    

    [SerializeField]
    float detectionDistance = 10.0f;

    [SerializeField]
    float killDistance = 2.0f;

    [SerializeField]
    float FOV = 180;

    bool hasEnded = false;
    bool updateAnimation = true;

    public bool isOnTheMove = false;
    // Start is called before the first frame update
    void Start()
    {
        baseOfOperations = transform.position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = gm.player.transform;
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
            agent = gameObject.AddComponent<NavMeshAgent>();

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnTheMove = agent.isStopped;
        checkKillPlayer();

        if (isOnTheMove && updateAnimation == true)
        {
            anim.Play("Walk");
            updateAnimation = false;
        }
        if (!isOnTheMove && updateAnimation == false)
        {
            anim.Play("Idle");
            updateAnimation = true;
        }
    }

    void checkKillPlayer()
    {
        Vector3 targetDir = player.position - transform.position;
        float dist = Vector3.Distance(player.position, transform.position);
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));
        if (angleToPlayer >= -(FOV / 2) && angleToPlayer <= (FOV / 2) && dist <= detectionDistance && !hasEnded)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, targetDir, out hit, detectionDistance, ~(1 << 8));
            if (hit.transform.name == player.name)
            {
                //end player
                Debug.Log(agent.destination);
                agent.SetDestination(player.position);
                agent.isStopped = false;
                if (dist <= killDistance)
                {
                    hasEnded = true;
                    gm.EndGame(EndMethod.Friendly);
                }
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, baseOfOperations) >= 2.0f)
                agent.SetDestination(baseOfOperations);
            else
            {
                agent.isStopped = true;
            }
        }
        if (hasEnded)
            Debug.DrawRay(transform.position, targetDir * dist, Color.red);
    }
}
