using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    GameObject Player;
    float PlayerDistance;
    float ItemRadius = 3.0f;

    private GameObject LeftArm;
    private GameObject LeftHand;

    private GameObject RightArm;
    private GameObject RightHand;

    private Quaternion initRightArmRot;
    private Quaternion initLeftArmRot;

    private PlayerStats ps;

    Rigidbody rb;

    string handName = "";
    //bool pickupInProgress = false;
    //bool dropInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        ps = Player.GetComponent<PlayerStats>();

        LeftArm = GameObject.Find("Player/PlayerCamera/LeftArm");
        RightArm = GameObject.Find("Player/PlayerCamera/RightArm");

        LeftHand = LeftArm.transform.Find("LeftHand").gameObject;
        RightHand = RightArm.transform.Find("RightHand").gameObject;

        initLeftArmRot = LeftArm.transform.localRotation;
        initRightArmRot = RightArm.transform.localRotation;

        rb = transform.GetComponent<Rigidbody>();

        if (ps.LeftHandFull == false)
        {
            LeftArm.transform.rotation = Quaternion.Euler(LeftArm.transform.eulerAngles.x, LeftArm.transform.eulerAngles.y, 140.0f);
            LeftArm.SetActive(false);
        }
        if (ps.RightHandFull == false)
        {
            RightArm.transform.rotation = Quaternion.Euler(RightArm.transform.eulerAngles.x, RightArm.transform.eulerAngles.y, 40.0f);
            RightArm.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10.0f)
            Destroy(gameObject);

        PlayerDistance = Vector3.Distance(Player.transform.position, transform.position);

        if (PlayerDistance <= ItemRadius)
        {
            if (ps.LeftHandFull == false && Input.GetKeyDown(KeyCode.Alpha1))
            {
                handName = "LeftHand";
                LeftArm.SetActive(true);

                transform.GetComponent<Collider>().enabled = false;
                transform.GetComponent<Rigidbody>().isKinematic = true;

                transform.parent = LeftHand.transform;
                transform.localPosition = new Vector3(0.1f, 0.1f, 0.1f);
                transform.localRotation = Quaternion.Euler(-10.0f, 0.0f, 0.0f);
                ps.LeftHandFull = true;
            }
            else if (ps.RightHandFull == false && Input.GetKeyDown(KeyCode.Alpha2))
            {
                handName = "RightHand";
                RightArm.SetActive(true);

                transform.GetComponent<Collider>().enabled = false;
                transform.GetComponent<Rigidbody>().isKinematic = true;

                transform.parent = RightHand.transform;
                transform.localPosition = new Vector3(0.1f, 0.1f, 0.1f);
                transform.localRotation = Quaternion.Euler(10.0f, 0.0f, 0.0f);
                ps.RightHandFull = true;
            }
            else if (ps.LeftHandFull == true && Input.GetKeyDown(KeyCode.Alpha1))
            {
                handName = "LeftHand";
                LeftArm.SetActive(false);

                transform.parent = null;
                ps.LeftHandFull = false;

                transform.GetComponent<Collider>().enabled = true;
                transform.GetComponent<Rigidbody>().isKinematic = false;

                rb.AddRelativeForce(Vector3.forward * 40.0f);
            }
            else if (ps.RightHandFull == true && Input.GetKeyDown(KeyCode.Alpha2))
            {
                handName = "RightHand";
                RightArm.SetActive(false);

                transform.parent = null;
                ps.RightHandFull = false;

                transform.GetComponent<Collider>().enabled = true;
                transform.GetComponent<Rigidbody>().isKinematic = false;

                rb.AddRelativeForce(Vector3.forward * 40.0f);
            }
        }

        if (LeftArm.activeSelf == true || RightArm.activeSelf == true)
        {
            revealArms();
        }

        if (LeftArm.activeSelf == false || RightArm.activeSelf == false)
        {
            hideArms();
        }
    }

    private void revealArms()
    {
        if (handName == "LeftHand")
        {
            LeftArm.transform.eulerAngles = Vector3.Lerp(LeftArm.transform.eulerAngles, new Vector3(LeftArm.transform.eulerAngles.x, LeftArm.transform.eulerAngles.y, initLeftArmRot.eulerAngles.z), 10.0f * Time.deltaTime);
        }
        else
        {
            RightArm.transform.eulerAngles = Vector3.Lerp(RightArm.transform.eulerAngles, new Vector3(RightArm.transform.eulerAngles.x, RightArm.transform.eulerAngles.y, initRightArmRot.eulerAngles.z), 10.0f * Time.deltaTime);
        }
    }

    private void hideArms()
    {
        if (handName == "LeftHand")
        {
            LeftArm.transform.eulerAngles = Vector3.Lerp(LeftArm.transform.eulerAngles, new Vector3(LeftArm.transform.eulerAngles.x, LeftArm.transform.eulerAngles.y, 0.0f), 3.0f * Time.deltaTime);

            LeftArm.transform.rotation = Quaternion.Euler(LeftArm.transform.eulerAngles.x, LeftArm.transform.eulerAngles.y, 140.0f);
        }
        else
        {
            RightArm.transform.eulerAngles = Vector3.Lerp(RightArm.transform.eulerAngles, new Vector3(RightArm.transform.eulerAngles.x, RightArm.transform.eulerAngles.y, 0.0f), 3.0f * Time.deltaTime);

            RightArm.transform.rotation = Quaternion.Euler(RightArm.transform.eulerAngles.x, RightArm.transform.eulerAngles.y, 40.0f);
        }
    }
}
