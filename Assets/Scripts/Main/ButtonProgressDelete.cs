using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonProgressDelete : MonoBehaviour
{
    public TMP_Text buttonText;
    bool alarmed = false;

    private void OnMouseDown()
    {
        if (!alarmed)
        {
            alarmed = true;
            buttonText.text = "Уверены?";
        }
        else
        {
            IntersceneMemory.instance.DeleteUserData();
            Application.Quit();
        }
    }
}
