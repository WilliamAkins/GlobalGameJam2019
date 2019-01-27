using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D Crosshairye;

    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (Crosshairye.width / 2);
        float yMin = (Screen.height / 2) - (Crosshairye.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, Crosshairye.width, Crosshairye.height), Crosshairye);
    }
}
