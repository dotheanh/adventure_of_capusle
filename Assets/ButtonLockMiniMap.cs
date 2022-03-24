using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLockMiniMap : MonoBehaviour
{
    private bool isLock = false;
    public Sprite locked, unlocked;


    public void OnToggleState()
    {
        isLock = !isLock;

        ((Image)GetComponent<Button>().targetGraphic).sprite = isLock ? unlocked : locked;
    }
}
