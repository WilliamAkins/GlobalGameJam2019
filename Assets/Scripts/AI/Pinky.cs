using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Pinky : MonoBehaviour //Drunk guy (path ABCD)
{
    
    NavMeshAgent agent;

    [SerializeField]
    List<Transform> points = new List<Transform>();

    int currentIndex = 0;

    [SerializeField]
    float changeDistance = 2.0f;
    bool changePosition = true;

    Transform player;

    [SerializeField]
    float changePositionTimer;

    [SerializeField]
    float detectionDistance = 10.0f;

    [SerializeField]
    float killDistance;

    [SerializeField]
    float FOV = 90;

    bool hasEnded = false;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        killDistance = changeDistance + 1.0f;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
            agent = gameObject.AddComponent<NavMeshAgent>();

        agent.SetDestination(points[0].position);
        player = gm.player.transform;
    }

    // Update is called once per frame
    void Update()
    {

        checkKillPlayer();
        if (Vector3.Distance(agent.destination, transform.position) <= changeDistance && changePosition)
        {
            updatePath();
        }
    }

    IEnumerator resetUpdateTimer(float f)
    {
        float endTime = Time.timeSinceLevelLoad + f;
        while(Time.timeSinceLevelLoad <= endTime)
        {
            yield return null;
        }
        changePosition = true;
    }

    void checkKillPlayer()
    {
        Vector3 targetDir = player.position - transform.position;
        float dist = Vector3.Distance(player.position, transform.position);
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));
        if(angleToPlayer >= -(FOV/2) && angleToPlayer <= (FOV/2) && dist <= detectionDistance && !hasEnded)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, targetDir, out hit, detectionDistance, ~(1 << 8) );
            if (hit.transform.name == player.name)
            {
                //end player
                agent.SetDestination(player.position);
                if(dist <= killDistance)
                {
                    hasEnded = true;
                    gm.EndGame(EndMethod.Drunk);
                }
            }
        }
        if(hasEnded)
            Debug.DrawRay(transform.position, targetDir * dist, Color.red);
    }

    void updatePath()
    {
        changePosition = false;
        
        currentIndex++;
        if(currentIndex > points.Count - 1)
        {
            currentIndex = 0;
        }
        agent.SetDestination(points[currentIndex].position);
        StartCoroutine(resetUpdateTimer(changePositionTimer));
    }
    
}
