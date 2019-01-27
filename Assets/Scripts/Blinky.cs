using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : MonoBehaviour
{
    GameObject Player;
    GameObject DoorToPeek;

    float playerDistance;
    float WarningRadius = 7.0f;
    [SerializeField]
    float CaughtRadius = 10.0f;

    float SecondIntervals = 0;

    bool DoorOpen = false;

    [SerializeField]
    float openDelay = 2.0f;

    [SerializeField]
    float closeDelay = 4.0f;

    [SerializeField]
    float spotTime = 2.0f;

    float currentSpotTime = 0.0f;

    bool canSeePlayer = false;

    bool canHearPlayer = false;

    GameManager gm;

    bool hasEnded = false;

    AudioSource audioSource;

    [SerializeField]
    List<AudioClip> clips;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        DoorToPeek = GameObject.Find("BlinkyDoor");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void checkKillPlayer()
    {
        Vector3 basePos = DoorToPeek.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 targetDir = Player.transform.position - basePos;
        targetDir.y += 1.0f;
        float dist = Vector3.Distance(Player.transform.position, basePos);
        float angleToPlayer = (Vector3.Angle(targetDir, basePos));
        if (angleToPlayer >= -(180) && angleToPlayer <= (180) && dist <= CaughtRadius && !hasEnded)
        {
            RaycastHit hit;
            Physics.Raycast(basePos, targetDir, out hit, CaughtRadius, ~(1 << 8));
            if (hit.transform.name == Player.name)
            {
                canSeePlayer = true;
                if (currentSpotTime > spotTime)
                {
                    hasEnded = true;
                    gm.EndGame(EndMethod.Paranoid);
                    Debug.Log("Caught by Paranoid Housemate");
                }
                    currentSpotTime += Time.deltaTime;

            }
            else
            {
                canSeePlayer = false;
                currentSpotTime -= Time.deltaTime;
            }
        }
        if (hasEnded)
            Debug.DrawRay(basePos, targetDir * dist, Color.red);
    }

    public void alertEnemy()
    {
        if (!canHearPlayer)
        {
            StartCoroutine(startOpenDoor());
        }
    }

    IEnumerator closeDoor()
    {
        float letsGetThisDone = Time.timeSinceLevelLoad + closeDelay;

        while (Time.timeSinceLevelLoad <= letsGetThisDone)
        {
            yield return null;
        }
        canHearPlayer = false;
    }

    IEnumerator startOpenDoor()
    {
        if (!canHearPlayer && clips.Count != 0) {
            if(!audioSource.isPlaying)
            {
                audioSource.clip = clips[Random.Range(0, clips.Count - 1)];
                audioSource.loop = false;
                audioSource.Play();
            }
        }
        float letsGetThisDone = Time.timeSinceLevelLoad + openDelay;

        while(Time.timeSinceLevelLoad <= letsGetThisDone)
        {
            yield return null;
        }

        canHearPlayer = true;

        StartCoroutine(closeDoor());
    }
    // Update is called once per frame
    void Update()
    {
        if (canHearPlayer)
        {
            DoorOpen = true;
        }
        else if (!canHearPlayer && !canSeePlayer)
        {
            DoorOpen = false;
        }

        playerDistance = Vector3.Distance(Player.transform.position, transform.position);
        
        if (playerDistance < CaughtRadius && DoorOpen)
        {
            //Play warning song
            //Peek through the door a little bit
            //Debug.Log("Caught");
            //gm.EndGame(EndMethod.Paranoid);
            checkKillPlayer();
        }

        if (DoorOpen == false)
        {
            if (DoorToPeek.transform.rotation.eulerAngles.y < 180)
                DoorToPeek.transform.Rotate(new Vector3(0, 1, 0) * (Time.deltaTime * 20));

            if (this.transform.rotation.eulerAngles.y > 120)
                this.transform.Rotate(new Vector3(0, -1, 0) * (Time.deltaTime * 20));
        }


        if (DoorOpen == true)
        {

            if (DoorToPeek.transform.rotation.eulerAngles.y > 150)
            {
                Debug.Log("Hittin 1");
                DoorToPeek.transform.Rotate(new Vector3(0, -20, 0) * (Time.deltaTime * 20)); //this needs adapting
            }

            if (this.transform.rotation.eulerAngles.y < 150)
            {
                Debug.Log("Hittin 2");
                this.transform.Rotate(new Vector3(0, 15, 0) * (Time.deltaTime * 20));//this needs adapting
            }
        }
            

        

    }

}
