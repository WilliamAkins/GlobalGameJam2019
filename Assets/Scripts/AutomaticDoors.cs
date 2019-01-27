using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoors : MonoBehaviour
{
    float speed = 3.0f;
    float Angle;
    Vector3 Direction;
    public bool DoorYayNay;

    //private void Update()
    //{


    //    if (transform.rotation.eulerAngles.y == 180)
    //    {
    //        Direction = -Vector3.up;
    //        Angle = 90;
    //    }
    //    else
    //    {
    //        Angle = 0;
    //        Direction = Vector3.up;
    //    }

    //    //else if (DoorYayNay == true)
    //    //{
    //    //    //Direction = Vector3.up;
    //    //}


    //    if (DoorYayNay == false)
    //    {
    //        if (Mathf.Round(transform.eulerAngles.y) != Angle)
    //        {
    //            transform.Rotate(Direction * speed);
    //        }
    Quaternion open = Quaternion.Euler(0, 90, 0);
    Quaternion close = Quaternion.Euler(0, 10, 0);

    float seconds = 0;

    private void Update()
    {
        if (DoorYayNay != false)
        {
            seconds += Time.deltaTime;

            if (seconds < 3)
            {
                OpenDoor();

            }
            //else if (seconds < 6)
            //{
            //    CloseDoor();
            //    Debug.Log("Door Closing.");
            //    seconds = 0;
            //    DoorYayNay = false;
            //}
        }

    }

    public void OpenDoor()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, open, (Time.deltaTime + 0.25f));
        seconds += Time.deltaTime;

    }
    public void CloseDoor()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, close, (Time.deltaTime + 0.25f));

    }
}

        //    }
        //}
        //    //Direction = -Vector3.up;








        //public GameObject[] People;
        //public float[] Distances;

        //GameObject player;
        //GameObject blinky;
        //GameObject pinky;
        //float pdistance;
        //float pdistance2;
        //float pdistance3;

        //private bool DoorOpen = true;

        //// Start is called before the first frame update
        //void Start()
        //{
        //    player = GameObject.Find("Player");
        //    blinky = GameObject.Find("Blinky Idle");
        //    pinky = GameObject.Find("Pinky");
        //}

        //// Update is called once per frame
        //void Update()
        //{

        //    pdistance = Vector3.Distance(player.transform.position, transform.position);
        //    if (pdistance <= 3.0f)
        //        DoorOpen = false;
        //    else if (pdistance >= 3.0f)
        //        DoorOpen = true;

        //    pdistance2 = Vector3.Distance(blinky.transform.position, transform.position);
        //    if (pdistance2 <= 3.0f)
        //        DoorOpen = false;
        //    else if (pdistance2 >= 3.0f)
        //        DoorOpen = true;

        //    pdistance3 = Vector3.Distance(pinky.transform.position, transform.position);
        //    if (pdistance3 <= 3.0f)
        //        DoorOpen = false;
        //    else if (pdistance3 >= 3.0f)
        //        DoorOpen = true;

        //}

    //}
