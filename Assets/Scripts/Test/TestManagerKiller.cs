using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerKiller : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(TestManager.instance.gameObject);
    }
}
