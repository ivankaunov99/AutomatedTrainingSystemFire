using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTestThemesScript : MonoBehaviour
{
    public string direction;

    private void OnMouseDown()
    {
        switch (direction)
        {
            case "forward":
                TestThemesManager.instance.FlipPageForward();
                break;
            case "back":
                TestThemesManager.instance.FlipPageBack();
                break;
        }
    }
}
