using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardManager : MonoBehaviour
{
    public GameObject[] cups;
    public TMP_Text[] cupTexts;

    private void Start()
    {
        for (int i = 0; i < cups.Length; i++)
        {
            cups[i].SetActive(false);
        }

        for (int i = 0; i < 4; i++)
        {
            if (IntersceneMemory.instance.testHighscores[i].stars == 5)
            {
                cups[i].SetActive(true);
            }
            cupTexts[i].text = "Набрано " + IntersceneMemory.instance.testHighscores[i].stars + 
                "/5 звезд за тест по теме \"" + IntersceneMemory.instance.themes[i] + "\"";
        }

        for (int i = 4; i < 7; i++)
        {
            if (IntersceneMemory.instance.totalCoins >= 100 + (i - 4) * 200)
            {
                cups[i].SetActive(true);
            }
            cupTexts[i].text = "Заработано " + IntersceneMemory.instance.totalCoins + "/" + (100 + (i - 4) * 200) 
                + " монет";
        }

        int sumStars = 0;
        for (int i = 0; i < IntersceneMemory.instance.testHighscores.Length; i++)
        {
            sumStars += IntersceneMemory.instance.testHighscores[i].stars;
        }

        for (int i = 7; i < 10; i++)
        {
            if (sumStars >= (10 + (i - 7) * 5))
            {
                cups[i].SetActive(true);
            }
            cupTexts[i].text = "Набрано суммарно " + sumStars + "/" + (10 + (i - 7) * 5)
                + " звезд";
        }
    }
}
