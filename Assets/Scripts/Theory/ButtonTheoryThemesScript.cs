using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTheoryThemesScript : MonoBehaviour
{
    public string direction;

    private void OnMouseDown()
    {
        switch (direction)
        {
            case "forward":
                TheoryThemesManager.instance.FlipPageForward();
                break;
            case "back":
                TheoryThemesManager.instance.FlipPageBack();
                break;
        }
    }
}
