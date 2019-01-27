using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    PlayerMovement pm;
    CameraController cc;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMovement>();
        cc = player.GetComponentInChildren<CameraController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame(EndMethod meth)
    {
        string Enemy = "";
        switch (meth)
        {
            case EndMethod.Drunk:
                Enemy = "your drunk housemate";
                break;
            case EndMethod.Friendly:
                Enemy = "your friendly housemate";
                break;
            case EndMethod.Furry:
                Enemy = "your furry housemate";
                break;
            case EndMethod.Paranoid:
                Enemy = "your paranoid housemate";
                break;

        }

        //pass string to end screen

        StartCoroutine(RestartLevelAfterNSecond(5.0f));
        
    }

    IEnumerator RestartLevelAfterNSecond(float f)
    {
        float endTime = Time.timeSinceLevelLoad + f;
        pm.enabled = false;
        cc.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        while (Time.timeSinceLevelLoad <= endTime)
        {
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum EndMethod
{
    Drunk,
    Paranoid,
    Friendly,
    Furry
}
