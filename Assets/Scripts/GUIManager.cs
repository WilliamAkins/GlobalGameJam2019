using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    Objectives objectives;
    TextMeshProUGUI objectivesText;

    RawImage endCard;

    RawImage startCard;

    bool showEnding = false;

    bool gameStarting = false;

    float endTime = 0.0f;
    float startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        objectives = GameObject.Find("GameManager").GetComponent<Objectives>();
        objectivesText = GameObject.Find("MainGUI/TopRightPanel/ObjectiveText").GetComponent<TextMeshProUGUI>();
        endCard = GameObject.Find("MainGUI/FinishScreen/EndCard").GetComponent<RawImage>();

        startCard = GameObject.Find("MainGUI/StartScreen/StartCard").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarting == false)
            hideStartCard();

        if (objectives.getEndGame() == false)
        {
            //display the objectives
            if (objectives.getObjectiveID() < 3)
                objectivesText.text = objectives.getCurrentObjectiveText();
        }
        else if (objectives.getEndGame() == true)
        {
            showEndCard();
        }
    }

    void hideStartCard()
    {
        startTime += 1.0f * Time.deltaTime;

        if (startTime >= 2.0f)
        {
            var tempColor = startCard.color;
            tempColor.a -= 0.5f * Time.deltaTime;
            startCard.color = tempColor;

            if (tempColor.a <= 0.0f)
            {
                gameStarting = false;
            }
        }
    }

    void showEndCard()
    {
        showEnding = true;

        var tempColor = endCard.color;
        tempColor.a += 1.0f * Time.deltaTime;
        endCard.color = tempColor;

        if (tempColor.a >= 1.0f)
        {
            endTime += 1.0f * Time.deltaTime;

            if (endTime >= 3.0f)
            {
                Debug.Log("leaving the game");
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(0);
            }
        }
    }
}