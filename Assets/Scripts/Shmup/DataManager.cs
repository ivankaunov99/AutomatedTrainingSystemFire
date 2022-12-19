using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public Text nameInputText;
    public static string playerName;
    public static string bestPlayerName;
    public static int bestScore;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        bestPlayerName = " ";
        bestScore = -1;
        LoadBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void GetInput()
    {
        playerName = nameInputText.text;
        LoadBestScore();
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string bestPlayerName;
    }

    public static void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/shmupsavefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            bestPlayerName = data.bestPlayerName;
        }
    }

    public static void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.bestPlayerName = bestPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/shmupsavefile.json", json);
    }

    public static void SetName()
    {
        playerName = Instance.nameInputText.text;
    }
}
