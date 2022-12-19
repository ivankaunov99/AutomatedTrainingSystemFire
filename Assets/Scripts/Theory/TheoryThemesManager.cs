using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheoryThemesManager : MonoBehaviour
{
    public static TheoryThemesManager instance;

    public GameObject[] button;
    public TMP_Text[] buttonText;
    public int pageNumber;

    private void Start()
    {
        instance = this;

        pageNumber = 0;
        ShowPage();
    }

    void ShowPage()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i + 3 * pageNumber < IntersceneMemory.instance.themes.Length)
            {
                button[i].SetActive(true);
                buttonText[i].text = IntersceneMemory.instance.themes[i + 3 * pageNumber];
                button[i].GetComponent<ButtonSceneChangeScript>().themeIndex = i + 3 * pageNumber;
            }
            else
            {
                button[i].SetActive(false);
            }
        }
    }

    public void FlipPageForward()
    {
        if ((pageNumber + 1) * 3 < IntersceneMemory.instance.themes.Length)
        {
            pageNumber++;
            ShowPage();
        }
    }

    public void FlipPageBack()
    {
        if (pageNumber > 0)
        {
            pageNumber--;
            ShowPage();
        }
    }
}
