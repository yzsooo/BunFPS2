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

    public void RemoveLoadedWeapon()
    {        
        // destroy exising weapon if it has one
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }
    }

    public void InputStartAttack1()
    {
        _battack1 = true;
    }

    public void InputStopAttack1()
    {
        _battack1 = false;
        if (!currentWeapon) { return; }
        currentWeapon.StopAttack1();
    }

    private void FixedUpdate()
    {
        ProcessAttack1();
    }

    void ProcessAttack1()
    {
        // if the player is getting input to attack, use the weapon's attack
        if (_battack1)
        {
            // if the player doesnt have any weapon then break out of the loop
            if (!currentWeapon) { Debug.Log("Weapon not loaded"); return; }
            currentWeapon.StartAttack1();
        }
    }

    public void InputReload()
    {
        if (!currentWeapon) { return; }
        currentWeapon.Reload();
    }

    public void InputMouseVector(Vector2 input)
    {
        if (!currentWeapon) { return; }
        currentWeapon.ViewmodelSwayer.UpdateMouseVector(input);
    }

    public void InputMovementVector(Vector2 input)
    {
        if (!currentWeapon) { return; }
        currentWeapon.ViewmodelSwayer.UpdateMovementVector(input);
    }
}
