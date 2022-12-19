using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public GameObject gameEndMenu;
    
    public AudioSource musicPlayer;
    public AudioClip okSound; 
    public TMP_Text continueButtonText;

    private GameManager gamemanager;
    private HeroController heroController;
    
    private int continuePrice = 20;

    private void Start()
    {
        gamemanager = FindObjectOfType<GameManager>();
        heroController = FindObjectOfType<HeroController>();
    }

    public void ShowEndMenu()
    {
        gameEndMenu.SetActive(true);
        continueButtonText.text = "Продолжить (" + continuePrice + "/" + IntersceneMemory.instance.coins + " монет)";
    }

    public void RestartGame()
    {
        musicPlayer.PlayOneShot(okSound);
        Thread.Sleep(500);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinueGame()
    {        
        if (IntersceneMemory.instance.coins >= continuePrice)
        {
            //отключить геймовер, вернуть на экран персонажа и увеличить жизни до 5. и убрать меню конца игры
            gamemanager.gameIsOver = false;
            heroController.gameObject.SetActive(true);
            heroController.health = 5;
            gameEndMenu.SetActive(false);
            //потом еще надо будет денег снимать за это. и цену наращивать. и денюжку сейвить
            IntersceneMemory.instance.coins -= continuePrice;
            continuePrice += 10;
            IntersceneMemory.instance.SaveUserData();
        }        
    }
}
