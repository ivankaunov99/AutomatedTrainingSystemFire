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
        continueButtonText.text = "���������� (" + continuePrice + "/" + IntersceneMemory.instance.coins + " �����)";
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
            //��������� ��������, ������� �� ����� ��������� � ��������� ����� �� 5. � ������ ���� ����� ����
            gamemanager.gameIsOver = false;
            heroController.gameObject.SetActive(true);
            heroController.health = 5;
            gameEndMenu.SetActive(false);
            //����� ��� ���� ����� ����� ������� �� ���. � ���� ����������. � ������� �������
            IntersceneMemory.instance.coins -= continuePrice;
            continuePrice += 10;
            IntersceneMemory.instance.SaveUserData();
        }        
    }
}
