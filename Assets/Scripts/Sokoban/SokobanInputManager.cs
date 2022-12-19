using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokobanInputManager : MonoBehaviour
{
    static public SokobanInputManager instance;

    void Start()
    {
        instance = this;

        StartCoroutine("WaitForUser");
    }

    IEnumerator WaitForUser()
    {
        string pressedKey = "null";
        //Debug.Log("Waiting coroutine starts");
        for (; ; )
        {
            yield return new WaitForSeconds(.001f);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                pressedKey = "up";
                break;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pressedKey = "left";
                break;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                pressedKey = "right";
                break;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                pressedKey = "down";
                break;
            }
        }
        ReadUserInput(pressedKey);
    }

    private void ReadUserInput(string pressedKey)
    {
        SokobanManager.instance.TryToMove(pressedKey);
        StartCoroutine("WaitForUser");
    }
}