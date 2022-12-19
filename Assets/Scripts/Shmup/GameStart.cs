using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject player;
    public GameObject ui;
    public GameObject startMenu;

    public AudioSource musicPlayer;
    public AudioClip okSound;

    public void StartTheGame()
    {
        musicPlayer.PlayOneShot(okSound);

        DataManager.SetName();

        gameManager.SetActive(true);
        player.SetActive(true);
        ui.SetActive(true);
        startMenu.SetActive(false);
    }
}
