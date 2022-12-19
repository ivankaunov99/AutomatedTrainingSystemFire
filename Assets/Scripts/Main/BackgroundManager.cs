using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager instance;

    public Sprite[] backgroundSprites;

    SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetBackground(int newBackgroundNumber)
    {
        m_SpriteRenderer.sprite = backgroundSprites[newBackgroundNumber];
        IntersceneMemory.instance.backgroundNumber = newBackgroundNumber;
        IntersceneMemory.instance.SaveUserData();
    }
}
