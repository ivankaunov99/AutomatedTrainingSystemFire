using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float damage = 1;

    void Update()
    {
        float speed = 2.5f;

        transform.Translate(Vector2.up * Time.deltaTime * speed);

        DestroyOutOfBounds();
    }

    void DestroyOutOfBounds()
    {
        float maxRangeX = 1.5f;
        float maxRangeY = 2.25f;
        float extraSpace = 0.01f;

        maxRangeX += extraSpace;
        maxRangeY += extraSpace;

        if ((transform.position.x > maxRangeX) | (transform.position.x < -maxRangeX)
            | (transform.position.y > maxRangeY) | (transform.position.y < -maxRangeY))
        {
            Destroy(gameObject);
        }
    }
}
