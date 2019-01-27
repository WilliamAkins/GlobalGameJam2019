using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeSpawner : MonoBehaviour
{
    [SerializeField]
    Transform endPoint;

    [SerializeField]
    GameObject clydePrefab;

    [SerializeField]
    float spawnTimer = 5.0f;

    [SerializeField]
    float speed = 5.0f;


    [SerializeField]
    float angularSpeed = 300.0f;

    float currentTimer;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = Time.timeSinceLevelLoad + spawnTimer ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= currentTimer)
        {
            Debug.Log("SPAWNINGGG");
            currentTimer += spawnTimer;
            GameObject spawned = Instantiate(clydePrefab, transform.position, transform.rotation);
            spawned.GetComponent<Clyde>().setup(endPoint.position, speed, angularSpeed);


        }

    }
}
