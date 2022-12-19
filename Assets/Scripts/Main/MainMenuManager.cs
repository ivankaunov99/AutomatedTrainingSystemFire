using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text starText;
    int stars = 0;
    
    void Start()
    {
        coinText.text = IntersceneMemory.instance.coins.ToString();
        for (int i = 0; i < IntersceneMemory.instance.testHighscores.Length; i++)
        {
            stars += IntersceneMemory.instance.testHighscores[i].stars;
        }
        starText.text = stars.ToString();
    }
}
