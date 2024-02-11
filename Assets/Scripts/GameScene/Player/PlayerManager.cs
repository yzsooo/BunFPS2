using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    bool bLockMouse = false;
    public bool MouseLock
    {
        get { return bLockMouse; }
    }

    public Transform playerTransform;
    public PlayerMovement movement;
    public Camera playerCamera;
    public PlayerLook look;
    public PlayerAttack attack;

    // should probably have a func that sets _pm to all the above classes

    public void FlipMouseLock()
    {
        bLockMouse = !bLockMouse;
        switch (bLockMouse)
        {
            case true:
                Cursor.lockState = CursorLockMode.Locked; break;
            case false:
                Cursor.lockState = CursorLockMode.None; break;
        }
    }
}
