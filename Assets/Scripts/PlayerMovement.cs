using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip left;
    public AudioClip right;

    [SerializeField]
    float normalMoveSpeed = 6.0f;

    [SerializeField]
    float stealthMoveSpeed = 3.0f;

    [SerializeField]
    float distanceFromFloor = 0.5f;

    AudioSource source;
    Animator animator;

    float moveSpeed;
    
    Rigidbody rb;

    bool stealth = false;

    bool isMovingBool = false;

    Vector3 vec = Vector3.zero;

    Vector3 previousPos = Vector3.zero;

    private StickyFingers stickyFinger;

    private GameObject leftLeg;
    private GameObject rightLeg;
    private short rotateDir = 1;
    private bool shouldReset = false;

    private bool foot = false;
    private float timer = 0;

    private bool limitFlip = false;
    private float flipTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        leftLeg = transform.Find("LeftLeg").gameObject;
        rightLeg = transform.Find("RightLeg").gameObject;

        moveSpeed = normalMoveSpeed;
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            gameObject.AddComponent<Rigidbody>();
            rb = GetComponent<Rigidbody>();
        }

        source = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();

        stickyFinger = GetComponent<StickyFingers>();
    }

    public bool isMoving()
    {
        return isMovingBool;
    }

    public bool getStealth() //this one is for William Akins
    {
        return stealth;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position != new Vector3(previousPos.x, transform.position.y, previousPos.z))
        {
            isMovingBool = true;
            if(!animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Sneak"))
            {
                animator.Play("Walk");
            }

        }
        else
        {
            isMovingBool = false;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
            {
                animator.Play("Idle");
            }
        }

        if (Input.GetButtonDown("Crouch"))
        {
            stealth = !stealth;
        }
        if (stealth)
        {
            moveSpeed = stealthMoveSpeed;
        }
        else
            moveSpeed = normalMoveSpeed;

        //Debug.Log(isMovingBool);

        //if the player is moving, then move the legs
        if (isMovingBool)
        {
            rotateLegs();
        }
        else if (shouldReset)
        {
            resetLegs();
        }
    }

    void FixedUpdate()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        if ((Horizontal != 0 || Vertical != 0) && checkIfGround())
        {
            float h = Horizontal * moveSpeed;
            float v = Vertical * moveSpeed;
            vec = new Vector3(h, rb.velocity.y, v);
            vec = Quaternion.Euler(transform.rotation.eulerAngles) * vec;
            rb.velocity = vec;
        }
        else if (checkIfGround())
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        previousPos = transform.position;

    }

    bool checkIfGround()
    {
        bool onGround = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.0f + distanceFromFloor))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            onGround = true;
        }
        return onGround;
    }

    private void rotateLegs()
    {
        float deltaRot;

        //Reduces volume for crouch
        if (getStealth() == true)
        {
            source.volume = 0.5f;
            deltaRot = 20.0f * Time.deltaTime;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Sneak"))
            {
                animator.Play("Sneak");
            }
        }
        else
        {
            source.volume = 1.0f;
            deltaRot = 180.0f * Time.deltaTime;
        }

        //Random pitch for sound variation
        source.pitch = Random.Range(1.0f, 1.5f);

        //Updating timer
        timer += Time.deltaTime;

        //indicates whether the legs have been moving
        shouldReset = true;

        leftLeg.transform.Rotate(new Vector3(deltaRot * rotateDir, 0.0f, 0.0f));
        rightLeg.transform.Rotate(new Vector3(-deltaRot * rotateDir, 0.0f, 0.0f));
        
        //once the legs have reached the rotation limit, flip the rotation
        if (Mathf.Abs(leftLeg.transform.eulerAngles.x) < 40.0f || Mathf.Abs(leftLeg.transform.eulerAngles.x) > 140.0f)
        {
            if (leftLeg.transform.eulerAngles.x < 40.0f)
                leftLeg.transform.eulerAngles = new Vector3(40.0f, leftLeg.transform.eulerAngles.y, leftLeg.transform.eulerAngles.z);

            if (leftLeg.transform.eulerAngles.x > 140.0f)
                leftLeg.transform.eulerAngles = new Vector3(140.0f, leftLeg.transform.eulerAngles.y, leftLeg.transform.eulerAngles.z);

            rotateDir *= -1;
            float vol = 0;
            //Switches between footstep sounds
            if (!getStealth())
                vol = 5;
            else
                vol = 2.5f;


            switch(foot)
            {
                case true:
                    {
                        source.PlayOneShot(left);
                        foot = !foot;

                        GameObject audVis = Instantiate(Resources.Load("AudioVisualParticles"), leftLeg.transform.Find("LeftFoot").transform.position, Quaternion.identity) as GameObject;
                        audVis.GetComponent<AudioVisualisation>().setVolume(vol, stickyFinger.getNumOfSuckItems());

                        break;
                    }
                case false:
                    {
                        source.PlayOneShot(right);
                        foot = !foot;

                        GameObject audVis = Instantiate(Resources.Load("AudioVisualParticles"), rightLeg.transform.Find("RightFoot").transform.position, Quaternion.identity) as GameObject;
                        audVis.GetComponent<AudioVisualisation>().setVolume(vol, stickyFinger.getNumOfSuckItems());
                        audVis.transform.rotation.eulerAngles.Set(90, 0, 0);

                        break;
                    }
            }
        }
    }

    private void resetLegs()
    {
        leftLeg.transform.eulerAngles = Vector3.Lerp(leftLeg.transform.eulerAngles, new Vector3(90.0f, leftLeg.transform.eulerAngles.y, leftLeg.transform.eulerAngles.z), 0.15f);

        rightLeg.transform.eulerAngles = Vector3.Lerp(rightLeg.transform.eulerAngles, new Vector3(90.0f, rightLeg.transform.eulerAngles.y, rightLeg.transform.eulerAngles.z), 0.15f);

        if (Mathf.Abs(leftLeg.transform.eulerAngles.x) == 90.0f && Mathf.Abs(rightLeg.transform.eulerAngles.x) == 90.0f)
        {
            shouldReset = false;
            rotateDir = 1;
        }
    }
}