using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    GameManager gm;

    PlayerMovement pm;

    [SerializeField]
    AudioClip danger;

    [SerializeField]
    AudioClip normal;

    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pm = gm.player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
