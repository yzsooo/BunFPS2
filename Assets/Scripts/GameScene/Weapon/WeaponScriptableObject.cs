using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Reference", menuName = "Weapon Scriptable Object")]
public class WeaponScriptableObject : ScriptableObject
{
    public string weaponName;
    public WeaponObject weaponPrefab;
    [Header("Stats")]
    public int roundsPerMagazine;
    public float roundsPerMinute;
    public float damage;
    public float reloadTime;

    [Header("Recoil")]
    public float RecoilAnglePerShot;
    public float RecoilMaxAngle;
    public float RecoilTimePerShot;

    [Header("Spread")]
    public float SpreadMaxAngle;
    public int SpreadShotsToMaxAngle;
    public float SpreadRecoverTime; 
    public float GetSpreadInterpolatePerShot()
    {
        return SpreadRecoverTime / SpreadShotsToMaxAngle;
    }
    public float GetSpreadAnglePerShot()
    {
        return SpreadMaxAngle / SpreadShotsToMaxAngle;
    }

    [Header("Sounds")]
    public AudioClip FireSound;
    public AudioClip ReloadSound;
}
