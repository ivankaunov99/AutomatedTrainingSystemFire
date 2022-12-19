using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    static public ShopManager instance;

    public GameObject[] backgroundButtons;
    public GameObject[] backgroundUnlockButtons;

    private void Start()
    {
        instance = this;

        for (int i = 0; i < backgroundButtons.Length; i++)
        {
            if (IntersceneMemory.instance.areBackgroundsUnlocked[i])
            {
                backgroundButtons[i].SetActive(true);
                backgroundUnlockButtons[i].SetActive(false);
            }
        }
    }

    public void TryToUnlockBackground(int backgroundNumber)
    {
        if (IntersceneMemory.instance.coins >= 100)
        {
            IntersceneMemory.instance.coins -= 100;
            IntersceneMemory.instance.areBackgroundsUnlocked[backgroundNumber] = true;
            IntersceneMemory.instance.SaveUserData();

            SceneManager.LoadScene("Shop");
        }
    }
}
