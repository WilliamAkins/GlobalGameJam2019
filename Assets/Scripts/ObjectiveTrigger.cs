using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [Header("The tag of the object that needs to collide with this trigger")]
    public string collisionTag;

    [Header("The length of time that the item/player must remain in the trigger")]
    public float waitTime = 0.0f;

    [Header("The ID of the objective that this is, in order to prevent it being done prematurely")]
    public int objectiveID;

    private Objectives objectives;

    private bool isColliding = false;
    private float timer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        objectives = GameObject.Find("GameManager").GetComponent<Objectives>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding)
        {
            timer += 1.0f * Time.deltaTime;
            if (timer >= waitTime)
            {
                Debug.Log("Object, has been inside the trigger for enough time.");

                //increment the objective ID thereofore moving to the next one
                objectives.updateObjectiveID(objectiveID + 1);

                //force game to end THIS IS HARD CODED
                if (objectiveID == 3)
                {
                    objectives.setEndGame(true);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectives.getObjectiveID() == objectiveID && collisionTag == other.transform.root.tag)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collisionTag == other.transform.root.tag)
        {
            isColliding = false;
            timer = 0.0f;
        }
    }
}