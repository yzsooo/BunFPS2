using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class WeaponRaycast : MonoBehaviour
{

    private WeaponScriptableObject _weaponStats;
    private Vector3 _cameraPosition;
    private Vector3 _cameraForward;

    // Shoot a raycast using the camera's current position and dircetion
    public RaycastHit GetRaycastAttack(Camera cam)
    {
        _cameraPosition = cam.transform.position;
        _cameraForward = cam.transform.forward;

        RaycastHit hit = ShootRaycast();

        return hit;
    }

    // returns the raycast from the camera with spread
    private RaycastHit ShootRaycast()
    {
        RaycastHit hit;
        RaycastHit rayFromCamera;
        Vector3 rayDirection = GetRaycastDirection();

        // actual direction
        Physics.Raycast(_cameraPosition, rayDirection, out hit, Mathf.Infinity, LayerMask.GetMask("Default"));
        Debug.DrawLine(_cameraPosition, hit.point, Color.red);

        // camera direction
        Physics.Raycast(_cameraPosition, _cameraForward, out rayFromCamera);
        Debug.DrawLine(_cameraPosition, rayFromCamera.point, Color.yellow);

        return hit;
    }

    // returns the direction of the raycast with spread 
    private Vector3 GetRaycastDirection()
    {
        Vector3 raycastDircetion = _cameraForward;

        return raycastDircetion;
    }



}
