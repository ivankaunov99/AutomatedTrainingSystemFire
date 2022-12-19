using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBackgroundUnlock : MonoBehaviour
{
    public int backgroundNumber;

    private void OnMouseDown()
    {
        ShopManager.instance.TryToUnlockBackground(backgroundNumber);
    }
}
