using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponRaycastSpread : MonoBehaviour
{
    public WeaponObject weapon;

    [SerializeField]
    int _spreadRampup;
    [SerializeField]
    float _spreadInterpolate;

    private void Update()
    {
        ProcessSpread();
    }

    void ProcessSpread()
    {
        // dont reduce spread if weapon is used
        if (weapon.attack.bIsFiring) { return; }
        // reduce spread interpolate overtime
        _spreadInterpolate -= Time.deltaTime;
        _spreadInterpolate = Mathf.Clamp(_spreadInterpolate, 0, _spreadInterpolate);
        // reduce spread rampup alongside interpolate
        _spreadRampup = Convert.ToInt32(Mathf.Lerp(0, weapon.weaponStats.SpreadShotsToMaxAngle, _spreadInterpolate));

    }

    public Vector3 GetSpreadVector()
    {
        Vector3 spreadVector;

        // if spread rampup is 0 (ie first shot since spread has been reset), no spread is added
        if (_spreadRampup == 0)
        {
            // add spread rampup and spread interpolate
            _spreadRampup++;
            _spreadRampup = Mathf.Clamp(_spreadRampup, 0, weapon.weaponStats.SpreadShotsToMaxAngle);;
            _spreadInterpolate = weapon.weaponStats.GetSpreadInterpolatePerShot() * _spreadRampup;
            return Vector3.zero;
        }
        // if spread rampup is more than 0, return a spread vector based on spread 
        _spreadRampup++;
        _spreadRampup = Mathf.Clamp(_spreadRampup, 0, weapon.weaponStats.SpreadShotsToMaxAngle);
        _spreadInterpolate = weapon.weaponStats.GetSpreadInterpolatePerShot() * _spreadRampup;

        // calculate spread vector
        Func<float> randomSpread = () => Random.Range(-weapon.weaponStats.GetSpreadAnglePerShot(), weapon.weaponStats.GetSpreadAnglePerShot()) * _spreadRampup;
        spreadVector = new Vector3(randomSpread(), randomSpread(), randomSpread());
        return spreadVector;
    }

}
