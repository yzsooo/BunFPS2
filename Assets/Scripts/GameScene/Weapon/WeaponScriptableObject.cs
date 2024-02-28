using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Reference", menuName = "Weapon Scriptable Object")]
public class WeaponScriptableObject : ScriptableObject
{
    public string weaponName;
    [Header("Stats")]
    public int roundsPerMagazine;
    public float roundsPerMinute;
    public float damage;
    public float reloadTime;

    [Header("Recoil")]
    public float RecoilAnglePerShot;
    public float RecoilMaxAngle;
    public float RecoilTimePerShot;

    [Header("Sounds")]
    public AudioClip FireSound;
    public AudioClip ReloadSound;
}
