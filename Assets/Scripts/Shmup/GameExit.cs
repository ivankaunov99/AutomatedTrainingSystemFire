using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class GameExit : MonoBehaviour
{
    public AudioSource musicPlayer;
    public AudioClip okSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            CloseGame();
        }
    }

    public void CloseGame()
    {
        musicPlayer.PlayOneShot(okSound);
        Thread.Sleep(500);
        SceneManager.LoadScene("MainMenu");
    }
}
