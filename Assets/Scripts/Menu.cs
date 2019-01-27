using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    Canvas canvas;


    float timer = -10;
    bool flip = false;
    int spaceCount = 0;
    RawImage image;

    bool switchTex = false;
    float fade = 0;
    bool fading = true;

    Color white = Color.white;
    Color black = Color.black;

    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip Introclip;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        audio = this.GetComponent<AudioSource>();
        image = canvas.transform.Find("RawImage").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flip)
        {
            timer += Time.deltaTime;
            cam.transform.Rotate(0, 0.01f, 0);
        }

        if (!flip)
        {
            timer -= Time.deltaTime;
            cam.transform.Rotate(0, -0.01f, 0);
        }

        if (timer > 10)
            flip = false;
        if (timer < -10)
            flip = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(audio.clip != Introclip)
            {
                audio.clip = Introclip;
                audio.Play();
                audio.loop = true;
            }
            spaceCount++;
            switchTex = true;
            fading = true;
        }

        if (switchTex)
        {
            if (fading)
            {
                fade += Time.deltaTime;
                image.GetComponent<RawImage>().color = Color.Lerp(white, black, fade);
            }

            if (fade > 1)
            {
                switch (spaceCount)
                {
                    case 1:
                        {

                            image.GetComponent<RawImage>().texture = Resources.Load("intro1") as Texture2D;
                            break;
                        }
                    case 2:
                        {
                            image.GetComponent<RawImage>().texture = Resources.Load("intro2") as Texture2D;
                            break;
                        }
                    case 3:
                        {
                            image.GetComponent<RawImage>().texture = Resources.Load("intro3") as Texture2D;
                            break;
                        }
                    case 4:
                        {
                            image.GetComponent<RawImage>().texture = Resources.Load("intro4") as Texture2D;
                            break;
                        }
                    case 5:
                        {
                            image.GetComponent<RawImage>().texture = Resources.Load("intro5") as Texture2D;
                            break;
                        }
                    case 6:
                        {
                            image.GetComponent<RawImage>().texture = Resources.Load("intro6") as Texture2D;
                            break;
                        }
                    case 7:
                        {
                            image.GetComponent<RawImage>().texture = Resources.Load("intro7") as Texture2D;
                            break;
                        }
                    case 8:
                        {
                            SceneManager.LoadScene("MainScene");
                            break;
                        }


                    default: break;
                }
                fading = false;
                fade = 0;
            }
            if (!fading)
            {
                fade += Time.deltaTime;
                image.GetComponent<RawImage>().color = Color.Lerp(black, white, fade);
                if (fade > 1)
                    switchTex = false;
            }
        }




            

        if(spaceCount >= 1)
        {
            canvas.transform.Find("Text").gameObject.SetActive(false);
            image.gameObject.SetActive(true);

        }

        


    }
}
