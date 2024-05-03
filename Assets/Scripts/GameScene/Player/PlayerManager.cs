using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerManager : MonoBehaviour
{
    bool bPlayerEnabled = true;
    public bool PlayerEnabled
    {
        get { return PlayerEnabled; }
        set { EnablePlayerControl(value); }
    }
    bool bLockMouse = false;
    public bool MouseLock
    {
        get { return bLockMouse; }
    }
    [Header("Player Components")]
    public PlayerEntity playerEntity;
    public Transform playerTransform;
    public PlayerMovement movement;
    public Camera playerCamera;
    public PlayerLook look;
    public PlayerAttack attack;
    [Header("External Player-related Components")]
    public PlayerHUDManager HUD;

    // should probably have a func that sets _pm to all the above classes

    // turn on/off the player's movement, look and attack
    void EnablePlayerControl(bool bEnable)
    {
        bPlayerEnabled = bEnable;
        movement.enabled = bPlayerEnabled;
        look.enabled = bPlayerEnabled;
        attack.enabled = bPlayerEnabled;
        playerEntity.HP.enabled = bPlayerEnabled;
    }

    // lock or unlock mouse cursor in game
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
