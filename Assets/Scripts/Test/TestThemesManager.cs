using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestThemesManager : MonoBehaviour
{
    public static TestThemesManager instance;

    public GameObject[] button;
    public TMP_Text[] buttonText;
    public int pageNumber;
    public TMP_Text[] starTexts;
    public GameObject[] stars;
    public GameObject[] starBackgrounds;

    void Start()
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
                stars[i].SetActive(true);
                starBackgrounds[i].SetActive(true);
                starTexts[i].text = IntersceneMemory.instance.testHighscores[i + 3 * pageNumber].stars.ToString() + "/5";
            }
            else
            {
                button[i].SetActive(false);
                stars[i].SetActive(false);
                starBackgrounds[i].SetActive(false);
                starTexts[i].text = "";
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
