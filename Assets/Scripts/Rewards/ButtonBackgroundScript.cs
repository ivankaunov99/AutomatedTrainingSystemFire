using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBackgroundScript : MonoBehaviour
{
    public int backGroundNumber;

    void OnMouseDown()
    {
        if (backGroundNumber != IntersceneMemory.instance.backgroundNumber)
        {
            BackgroundManager.instance.SetBackground(backGroundNumber);
        }
        else
        {
            BackgroundManager.instance.SetBackground(0);
        }
    }
}
