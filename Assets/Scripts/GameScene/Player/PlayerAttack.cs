using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAttack : MonoBehaviour
{
    // External Components
    public PlayerManager pm;
    Camera _cam;

    // current loaded weapon
    public WeaponObject currentWeapon;

    // attack management vars
    bool _battack1 = false;

    private void Awake()
    {
        _cam = pm.playerCamera;
    }

    // Load a new weapon by removing existing weapon if it has any, then instantiating a new one and setting that as the new weapon
    public void LoadWeapon(WeaponObject newWeaponObject)
    {
        RemoveLoadedWeapon();
        // instantiate new weapon as the child of this object
        currentWeapon = Instantiate(newWeaponObject);
        currentWeapon.transform.parent = this.transform;
        // reset the transform of the new weapon
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        // set camera stack to add the new weapon
        // weapon camera is automatically removed from the stack when its unloaded so no need to worry about doing the opposite when unloading
        Camera weaponCamera = currentWeapon.GetComponent<Camera>();
        pm.playerCamera.GetUniversalAdditionalCameraData().cameraStack.Add(weaponCamera);

        // set currentWeapon's player variables
        currentWeapon.cam = _cam;
        currentWeapon.playerAttack = this;
    }
    
    // remove player's equipped weapon by destroying the gameObject
    public void RemoveLoadedWeapon()
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }
    }

    // set _battack1 to true when the player has Attack1 input
    public void InputStartAttack1()
    {
        _battack1 = true;
    }

    // set battack1 to false, and call the stopattack1 function in the equipped weapon
    public void InputStopAttack1()
    {
        _battack1 = false;
        if (currentWeapon) { currentWeapon.StopAttack1(); }
    }

    private void FixedUpdate()
    {
        if (currentWeapon)
        {
            ProcessWeaponEquipped();
        }
    }
    
    // process Attack1 input in the equipped weapon
    void ProcessWeaponEquipped()
    {
        ProcessAttack1();
    }

    // If the player is inputting Attack1, use the equipped weapon's attack
    void ProcessAttack1()
    {
        if (_battack1)
        {
            // if the player doesnt have any weapon then break out of the loop
            if (!currentWeapon) { Debug.Log("Weapon not loaded"); return; }
            currentWeapon.StartAttack1();
        }
    }

    // If the player has a weapon equipped, reload the equipped weapon
    public void InputReload()
    {
        if (!currentWeapon) { return; }
        currentWeapon.Reload();
    }

    // update the mouse input to the viewmodel swayer
    public void InputMouseVector(Vector2 input)
    {
        if (!currentWeapon) { return; }
        currentWeapon.ViewmodelSwayer.UpdateMouseVector(input);
    }

    // update the movement input to the viewmodel swayer
    public void InputMovementVector(Vector2 input)
    {
        if (!currentWeapon) { return; }
        currentWeapon.ViewmodelSwayer.UpdateMovementVector(input);
    }
}
