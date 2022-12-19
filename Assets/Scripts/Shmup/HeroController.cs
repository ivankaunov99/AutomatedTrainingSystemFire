using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float health;

    private bool isFocused = false;
    public GameObject bullet;
    private float maxShootTimer = 0.2f;
    private float shootTimer = 0.2f;
    float maxRangeX = 1.5f;
    float maxRangeY = 2.25f;
    float unfocusedSpeed = 4.0f;
    float focusedSpeed = 1.5f;
    private float redTime = 0f;
    private float maxRedTime = 0.3f;
    private GameManager gamemanager;
    public AudioSource playerSoundPlayer;
    public AudioClip shootSound;
    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Renderer thisRenderer = GetComponent<Renderer>();
        redTime -= Time.deltaTime;
        if (redTime <= 0f)
        {
            thisRenderer.material.SetColor("_Color", Color.white);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isFocused = true;
        }

        switch (isFocused)
        {
            case true:
                MoveCharacter(focusedSpeed);
                break;
            case false:
                MoveCharacter(unfocusedSpeed);
                break;
        }

        if (shootTimer >= 0)
        {
            shootTimer -= Time.deltaTime;
        }

        Shoot();

        isFocused = false;
    }

    void MoveCharacter(float speed)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector2.up * Time.deltaTime * speed * verticalInput);
        transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
        NoOutOfBounds();
    }

    void NoOutOfBounds()
    {  
        if (transform.position.x < -maxRangeX)
        {
            transform.position = new Vector2(-maxRangeX, transform.position.y);
        }
        if (transform.position.x > maxRangeX)
        {
            transform.position = new Vector2(maxRangeX, transform.position.y);
        }
        if (transform.position.y < -maxRangeY)
        {
            transform.position = new Vector2(transform.position.x, -maxRangeY);
        }
        if (transform.position.y > maxRangeY)
        {
            transform.position = new Vector2(transform.position.x, maxRangeY);
        }
    }

    void Shoot()
    {
        if ((Input.GetKey(KeyCode.Z)) && (shootTimer <= 0f))
        {
            if (isFocused)
            {
                ShootFocused();
            }
            if (!isFocused)
            {
                ShootUnfocused();
            }
            shootTimer += maxShootTimer;
            bullet.transform.rotation = Quaternion.identity;
        }
    }

    void ShootFocused()
    {
        playerSoundPlayer.PlayOneShot(shootSound);
        Instantiate(bullet, transform.position + Vector3.up * 0.5f + Vector3.left * 0.03f, bullet.transform.rotation);
        Instantiate(bullet, transform.position + Vector3.up * 0.5f + Vector3.right * 0.03f, bullet.transform.rotation);
    }

    void ShootUnfocused()
    {
        playerSoundPlayer.PlayOneShot(shootSound);
        Instantiate(bullet, transform.position + Vector3.up * 0.5f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 10));
        Instantiate(bullet, transform.position + Vector3.up * 0.5f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 340));
        Instantiate(bullet, transform.position + Vector3.up * 0.5f, bullet.transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            playerSoundPlayer.PlayOneShot(deathSound);
            EnemyBulletBehavior enemyBulletScript = collision.GetComponent<EnemyBulletBehavior>();
            health -= enemyBulletScript.damage;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                //умираем. значит тут и надо записать рекорды в файл.
                DataManager.SaveBestScore();

                gamemanager.gameIsOver = true;
                this.gameObject.SetActive(false);
            }

            Renderer thisRenderer = GetComponent<Renderer>();
            thisRenderer.material.SetColor("_Color", Color.red);
            redTime = maxRedTime;
        }
    }
}
