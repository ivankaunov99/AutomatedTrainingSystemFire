using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestResultManager : MonoBehaviour
{
    public TMP_Text resultTitleText;
    public TMP_Text correctAnswersCountText;
    public TMP_Text pointsCountText;
    public TMP_Text starsCountText;
    public TMP_Text coinsCountText;

    public GameObject congratsText;

    public int stars;
    public int coins;

    private void Start()
    {
        stars = (int)((TestManager.instance.score / TestManager.instance.questions.Length) * 5);
        coins = (int)TestManager.instance.score * 10;

        if (stars == 5)
        {
            congratsText.SetActive(true);
        }

        IntersceneMemory.instance.coins += this.coins;
        IntersceneMemory.instance.totalCoins += this.coins;
        if (IntersceneMemory.instance.testHighscores[IntersceneMemory.instance.themeIndex].stars < this.stars)
        {
            IntersceneMemory.instance.testHighscores[IntersceneMemory.instance.themeIndex].stars = this.stars;
        }

        resultTitleText.text = "Ваш результат:";
        correctAnswersCountText.text = "Правильно дан ответ на вопросы: " + TestManager.instance.correctAnswerCounter + "/" 
            + TestManager.instance.questions.Length;
        pointsCountText.text = "Набрано баллов: " + TestManager.instance.score + "/"
            + TestManager.instance.questions.Length;
        starsCountText.text = "Получено звезд: " + stars;
        coinsCountText.text = "Заработано монет: " + coins;

        IntersceneMemory.instance.SaveUserData();
    }
}
