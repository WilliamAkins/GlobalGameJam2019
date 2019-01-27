using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishSpawner : MonoBehaviour
{
    public int numOfRubbishItems = 100;

    bool checkIfCeiling(Vector3 pos)
    {
        bool hasCeiling = false;
        RaycastHit hit;
        if (Physics.Raycast(pos, transform.TransformDirection(Vector3.up), out hit, 4.0f))
        {
            //Debug.DrawRay(pos, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
            hasCeiling = true;
        }
        return hasCeiling;
    }

    bool checkIfGround(Vector3 pos)
    {
        bool onGround = false;
        RaycastHit hit;
        if (Physics.Raycast(pos, transform.TransformDirection(Vector3.down), out hit, 1.0f))
        {
            //Debug.DrawRay(pos, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            onGround = true;
        }
        return onGround;
    }

    // Start is called before the first frame update
    void Start()
    {
        float yPos = 0.4f;

        for (int i = 0; i < numOfRubbishItems; i++)
        {
            if (i > numOfRubbishItems / 2)
                yPos = 3.9f;

            Vector3 newPos;
            do {
                newPos = new Vector3(Random.Range(-25.0f, 25.0f), yPos, Random.Range(-25.0f, 25.0f));
            } while (!checkIfGround(newPos) || !checkIfCeiling(newPos));

            GameObject item = Instantiate(Resources.Load("StickyItem" + Random.Range(1, 7)), newPos, Quaternion.identity) as GameObject;
            item.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, Random.Range(0.0f, 360.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}