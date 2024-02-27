using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectAttackVisualSoundEffects : MonoBehaviour
{
    public WeaponObject weapon;

    public Transform MuzzlePosition;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitParticle;
    public GameObject BulletHoleDecal;

    [Header("Sounds")]
    [SerializeField]
    AudioClip _fireSound;
    AudioClip _reloadSound;

    //public void PlayWeaponVisualSoundEffects(RaycastHit hit)
    //{
    //    // play animation

    //    // play muzzle flash particle effect

    //    // play bullet hit particle

    //    // play sounds
        
    //    // show hitmarker on HUD
    //}

    public void PlayWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation animationToPlay)
    {
        switch (animationToPlay)
        {
            case WeaponObjectAnimationManager.weaponAnimation.Fire:
            {
                PlayFireAnimation();
                break;
            }
        }
    }

    void PlayFireAnimation()
    {
        weapon.weaponAnimation.PlayerWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.Fire);
        // play fire sound
        SoundPlayer.SoundPlayerInstance.PlaySound(_fireSound, transform.position, 1.0f);
        // play muzzle flash particle
        ParticleSystem muzzleflashInstance = Instantiate(MuzzleFlash, MuzzlePosition.position, weapon.ViewmodelCamera.transform.rotation);
        muzzleflashInstance.transform.parent = weapon.ViewmodelCamera.transform;
    }

    public void CreateBulletHoleDecal(RaycastHit hit)
    {
        // play bullet hit particle at the hit position
        ParticleSystem bulletHitParticleInstance = Instantiate(HitParticle, hit.point, Quaternion.Euler(hit.normal));

        // spawn bullet decal that decays overtime
        GameObject bulletholeDecal = Instantiate(BulletHoleDecal, hit.point + (hit.normal * 0.01f), Quaternion.FromToRotation(Vector3.back, hit.normal));
        Destroy(bulletholeDecal, 10f);
    }
}
