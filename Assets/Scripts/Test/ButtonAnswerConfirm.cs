using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnswerConfirm : MonoBehaviour
{
    void OnMouseDown()
    {
        TestManager.instance.FinishQuestion();
    }
}
