using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTestAnswerScript : MonoBehaviour
{
    public int answerNumber;
    public bool isPressed { private set; get; }

    SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        isPressed = false;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        isPressed = !isPressed;
        MarkButton();
    }

    public void MarkButton()
    {
        if (isPressed)
        {
            m_SpriteRenderer.color = new Color(0, 0, 1, (float)0.2);
        }
        else
        {
            m_SpriteRenderer.color = new Color(1, 1, 1);
        }
    }

    public void UnpressButton()
    {
        isPressed = false;        
    }

    private void Update()
    {
        if (!isPressed)
        {
            m_SpriteRenderer.color = new Color(1, 1, 1);
        }
    }
}
