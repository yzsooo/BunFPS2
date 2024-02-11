using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectAttackVisualSoundEffects : MonoBehaviour
{
    public WeaponObject weapon;

    public Transform MuzzlePosition;
    public ParticleSystem hitParticle;
    public GameObject bulletHoleDecal;

    public void PlayWeaponVisualSoundEffects(RaycastHit hit)
    {
        // play animation

        // play muzzle flash particle effect

        // play bullet hit particle

        // play sounds
        
        // show hitmarker on HUD
    }
}
