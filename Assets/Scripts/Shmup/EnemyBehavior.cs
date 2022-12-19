using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBehavior : MonoBehaviour
{
    public float health = 70;
    private float redTime = 0f;
    private float maxRedTime = 0.3f;
    public GameObject bullet;
    private float maxShootTimer = 2f;
    private float shootTimer = 2f;
    private GameManager gamemanager;
    public double shootAngle;

    public AudioSource audioPlayer;
    public AudioClip enemyShoot;
    public AudioClip enemyDeath;

    void EnemyShoot()
    {
        audioPlayer.PlayOneShot(enemyShoot);
    }

    void EnemyDeath()
    {
        //audioPlayer.PlayOneShot(enemyDeath);
    }

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = FindObjectOfType<GameManager>();
        shootTimer += UnityEngine.Random.Range(-1f, 1f);
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

        shootTimer -= Time.deltaTime;
        if (gamemanager.gameIsOver == false)
        {
            Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HeroBullet")
        {
            HeroBulletBehavior heroBulletScript = collision.GetComponent<HeroBulletBehavior>();
            health -= heroBulletScript.damage;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                EnemyDeath();
                gamemanager.score += 10;
                gamemanager.CompareScore();
                Destroy(gameObject);
            }
            Renderer thisRenderer = GetComponent<Renderer>();
            thisRenderer.material.SetColor("_Color", Color.red);
            redTime = maxRedTime;
        }
    }

    void Shoot()
    {
        shootAngle = FindShootAngle();

        int[] numbersArray = new int[] {5, 3, 2};
        string[] methodsArray = new string[] {"ShootOdd", "ShootEven", "ShootAll" };

        if (shootTimer <= 0)
        {
            EnemyShoot();
            RandomMethod(numbersArray, methodsArray);
            shootTimer += maxShootTimer; 
            shootTimer += UnityEngine.Random.Range(-1f, 1f);
        }

        bullet.transform.rotation = Quaternion.identity;
    }

    public void RandomMethod(int[] numberInput, string[] methodInput)
    {
        bool invoked = false;

        for (int i = 1; i < numberInput.Length; i++)
        {
            numberInput[i] += numberInput[i - 1];
        }

        float rn = UnityEngine.Random.Range(0f, numberInput[numberInput.Length - 1]);

        for (int i = numberInput.Length - 1; i > 0; i--)
        {
            if (invoked == true)
            {
                break;
            }

            if (rn > numberInput[i - 1])
            {
                Invoke(methodInput[i], 0);
                invoked = true;
            }
        }

        if (invoked == false)
        {
            Invoke(methodInput[0], 0);
        }
    }

    double FindShootAngle()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        Vector3 vectorToPlayer = playerPosition - (transform.position + Vector3.down * 0.25f);
        var tan = vectorToPlayer.y / vectorToPlayer.x;
        var angle = Math.Atan(tan) * 180 / Math.PI;
        angle = Math.Abs(angle);
        if ((vectorToPlayer.x >= 0) && (vectorToPlayer.y >= 0))
        {
            angle = 270 + angle;
        }
        else
        if ((vectorToPlayer.x >= 0) && (vectorToPlayer.y <= 0))
        {
            angle = 270 - angle;
        }
        else
        if ((vectorToPlayer.x <= 0) && (vectorToPlayer.y <= 0))
        {
            angle = 90 + angle;
        }
        else
        if ((vectorToPlayer.x <= 0) && (vectorToPlayer.y >= 0))
        {
            angle = 90 - angle;
        }

        return angle;
    }

    void ShootOdd ()
    {
        bullet.transform.Rotate(new Vector3(0, 0, (float)shootAngle - 12));
        Instantiate(bullet, transform.position + Vector3.down * 0.25f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 12));
        Instantiate(bullet, transform.position + Vector3.down * 0.25f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 12));
        Instantiate(bullet, transform.position + Vector3.down * 0.25f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 360 - (float)shootAngle - 12));
    }

    void ShootEven ()
    {
        bullet.transform.Rotate(new Vector3(0, 0, (float)shootAngle - 18));
        Instantiate(bullet, transform.position + Vector3.down * 0.25f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 12));
        Instantiate(bullet, transform.position + Vector3.down * 0.25f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 12));
        Instantiate(bullet, transform.position + Vector3.down * 0.25f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 12));
        Instantiate(bullet, transform.position + Vector3.down * 0.25f, bullet.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, 360 - (float)shootAngle - 18));
    }

    void ShootAll ()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(bullet, transform.position + Vector3.down * 0.25f, bullet.transform.rotation);
            bullet.transform.Rotate(new Vector3(0, 0, 12));
        }
    }
}
