using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlideManager : MonoBehaviour
{
    public SpriteRenderer m_SpriteRenderer;

    public static SlideManager instance;
    public Sprite[] theme0;
    public Sprite[] theme1;
    public Sprite[] theme2;
    public Sprite[] theme3;
    Sprite[] chosenTheme;
    public TMP_Text slideCounter;
    int slideNumber;

    private void Start()
    {
        instance = this;

        switch (IntersceneMemory.instance.themeIndex)
        {
            case 0:
                chosenTheme = theme0;
                break;
            case 1:
                chosenTheme = theme1;
                break;
            case 2:
                chosenTheme = theme2;
                break;
            case 3:
                chosenTheme = theme3;
                break;
        }

        slideNumber = 0;
        //m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = chosenTheme[slideNumber];

        SlideCounterUpdate();
    }

    public void MoveForward()
    {
        if (slideNumber < chosenTheme.Length - 1)
        {
            slideNumber++;
        }
        m_SpriteRenderer.sprite = chosenTheme[slideNumber];

        SlideCounterUpdate();
    }

    public void MoveBack()
    {
        if (slideNumber > 0)
        {
            slideNumber--;
        }
        m_SpriteRenderer.sprite = chosenTheme[slideNumber];

        SlideCounterUpdate();
    }

    void SlideCounterUpdate()
    {
        slideCounter.text = slideNumber + 1 + "/" + chosenTheme.Length;
    }
}
