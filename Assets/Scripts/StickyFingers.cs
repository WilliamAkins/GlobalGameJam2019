using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyFingers : MonoBehaviour
{
    private List<GameObject> stickies = new List<GameObject>();

    private float offsetRange = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onCollisionOccuring(GameObject trigger, GameObject col)
    {
        Debug.Log("Collision with a sticky detected! Preparing to stick");
        stickies.Add(col.gameObject);

        col.gameObject.GetComponent<BoxCollider>().enabled = false;
        col.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        //make the item a child of the player limb that collided
        col.gameObject.transform.parent = trigger.transform;

        //set the initial position of the item
        col.gameObject.transform.position = new Vector3(trigger.transform.position.x + Random.Range(-offsetRange, offsetRange), trigger.transform.position.y, trigger.transform.position.z + Random.Range(-offsetRange, offsetRange));
    }

    public int getNumOfSuckItems()
    {
        return stickies.Count;
    }
}