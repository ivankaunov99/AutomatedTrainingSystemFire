using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static public MusicManager instance;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }
}
