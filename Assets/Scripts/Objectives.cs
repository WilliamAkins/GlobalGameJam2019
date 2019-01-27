using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    public string[] objectiveText;

    //current object that needs to be completed
    private int objectiveID = 0;

    private bool endGame = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getObjectiveID()
    {
        return objectiveID;
    }

    public string getCurrentObjectiveText()
    {
        return objectiveText[objectiveID];
    }

    public void updateObjectiveID(int newObjectiveID)
    {
        if (newObjectiveID == 3)
            endGame = true;

        objectiveID = newObjectiveID;
    }

    public int getTotalNumOfObjectives()
    {
        return objectiveText.Length;
    }

    public bool getEndGame()
    {
        return endGame;
    }

    public void setEndGame(bool newEndGame)
    {
        endGame = newEndGame;
    }
}