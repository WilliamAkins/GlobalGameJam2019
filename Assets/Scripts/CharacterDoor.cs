using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDoor : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Collider[] area = Physics.OverlapSphere(transform.position, 4.0f);
        for (int i = 0; i < area.Length; i++)
        {
            if (area[i].gameObject.tag == "Door")
            {
                if (area[i].gameObject.GetComponent<AutomaticDoors>().DoorYayNay == false)
                {
                    area[i].gameObject.GetComponent<AutomaticDoors>().DoorYayNay = true;
                }


            }
        }
    }
}
