using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideDoor : MonoBehaviour
{
    
    GameObject player;
    GameObject blinky;
    GameObject pinky;
    float pdistance;
    float pdistance2;
    float pdistance3;

    private bool DoorOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        blinky = GameObject.Find("Blinky Idle");
        pinky = GameObject.Find("Pinky");

    }

    // Update is called once per frame
    void Update()
    {

        pdistance = Vector3.Distance(player.transform.position, transform.position);
        if (pdistance <= 3.0f)
            DoorOpen = false;
        else if (pdistance >= 3.0f)
            DoorOpen = true;

        //pdistance2 = Vector3.Distance(blinky.transform.position, transform.position);
        //if (pdistance2 <= 3.0f)
        //    DoorOpen = false;
        //else if (pdistance2 >= 3.0f)
        //    DoorOpen = true;

        //pdistance3 = Vector3.Distance(pinky.transform.position, transform.position);
        //if (pdistance3 <= 3.0f)
        //    DoorOpen = false;
        //else if (pdistance3 >= 3.0f)
        //    DoorOpen = true;

        //Vector3 rotationVector = new Vector3(0, 30 + Time.deltaTime, 0);
        Quaternion openInside = Quaternion.Euler(0, 180, 0);
        Quaternion closeInside = Quaternion.Euler(0, 90, 0);



        if (DoorOpen == true)
            transform.rotation = Quaternion.Slerp(transform.rotation, openInside, (Time.deltaTime + 0.25f));

        if (DoorOpen == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closeInside, (Time.deltaTime + 0.25f));

        }
    }

}
