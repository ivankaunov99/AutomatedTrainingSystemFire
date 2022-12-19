using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChangeScript : MonoBehaviour
{
    public string sceneName;
    public int themeIndex;

    void OnMouseDown()
    {
        IntersceneMemory.instance.themeIndex = this.themeIndex;
        SceneManager.LoadScene(sceneName);
    }
}
