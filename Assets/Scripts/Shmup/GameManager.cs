using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    private int enemyCount;
    public TMP_Text scoreText;
    public TMP_Text hpText;
    public TMP_Text waveText;
    public TMP_Text bestScoreText;
    public int score;
    public int waveCount;
    public bool gameIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        waveCount = 0;
        score = -10;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver == true)
        {
            FindObjectOfType<GameEnd>().ShowEndMenu();
        }

        scoreText.text = "Счет: " + score;
        if (gameIsOver == false)
        {
            hpText.text = "Жизни: " + FindObjectOfType<HeroController>().health;
        }
        else
        {
            hpText.text = "Жизни: " + 0;
        }
        bestScoreText.text = DataManager.bestPlayerName + ": " + DataManager.bestScore;

        enemyCount = FindObjectsOfType<EnemyBehavior>().Length;
        if (enemyCount == 0)
        {
            waveCount++;
            score += (waveCount - 1) * 5 + 10;
            CompareScore();
            SpawnNewWave((int)(waveCount / 3) + 2);
            enemyCount = FindObjectsOfType<EnemyBehavior>().Length;
        }

        waveText.text = "Волна: " + waveCount;
    }

    void SpawnNewWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        float randX = Random.Range(-1.5f, 1.5f);
        float randY = Random.Range(1f, 2f);
        Instantiate(enemy, new Vector3(randX, randY), enemy.transform.rotation);
    }

    public void CompareScore()
    {
        if (score > DataManager.bestScore)
        {
            DataManager.bestScore = score;
            DataManager.bestPlayerName = DataManager.playerName;
        }
    }
}
