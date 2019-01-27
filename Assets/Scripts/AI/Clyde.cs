using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clyde : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    bool hasEnded = false;
    GameManager gm;
    [SerializeField]
    Transform end;
    [SerializeField]
    float detectionDistance = 2.0f;

    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        Debug.Log(agent);
    }

    public void setup(Vector3 target, float speed, float angularSpeed)
    {
        agent.angularSpeed = angularSpeed;
        agent.speed = speed;
        agent.SetDestination(target);
    }
    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath)
        {
            if (Vector3.Distance(gm.player.transform.position, transform.position) <= detectionDistance && !hasEnded)
            {
                hasEnded = true;
                gm.EndGame(EndMethod.Furry);
            }

            if (Vector3.Distance(transform.position, agent.destination) <= 1.0f)
            {
                Debug.Log("Goodbye cruel world");
                endSelf();
            }
        }
    }

    void endSelf()
    {
        Destroy(this.gameObject);
    }
}
