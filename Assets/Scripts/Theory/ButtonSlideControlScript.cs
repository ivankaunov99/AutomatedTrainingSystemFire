using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSlideControlScript : MonoBehaviour
{
    public string direction;

    private void OnMouseDown()
    {
        switch (direction)
        {
            case "forward":
                SlideManager.instance.MoveForward();
                break;
            case "back":
                SlideManager.instance.MoveBack();
                break;
        }
    }
}
