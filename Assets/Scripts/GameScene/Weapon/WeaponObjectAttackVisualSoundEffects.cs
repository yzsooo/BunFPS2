using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponObjectAttackVisualSoundEffects : MonoBehaviour
{
    public WeaponObject weapon;

    [Header("Muzzle Flash")]
    public Transform MuzzlePosition;
    public ParticleSystem MuzzleFlash;
    [Header("Hit impact")]
    public ParticleSystem HitParticle;
    public GameObject BulletHoleDecal;

    float _recoilRecoverMultiplier = 2.0f;

    [Header("Sounds")]
    [SerializeField]
    AudioClip _fireSound , _reloadSound;

    void FixedUpdate()
    {
        AnimateHUDCrosshair();
    }

    void AnimateHUDCrosshair()
    {
        weapon.playerAttack.pm.HUD.HUDCrosshair.SetCrosshairMaxSize(weapon.weaponStats.SpreadMaxAngle);
        weapon.playerAttack.pm.HUD.HUDCrosshair.SetCrosshairSize(weapon.attack.RaycastSpread.SpreadInterpolate);
    }

    public void PlayWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation animationToPlay)
    {
        Debug.Log(animationToPlay);
        switch (animationToPlay)
        {
            case WeaponObjectAnimationManager.weaponAnimation.Fire:
                {
                    PlayFireAnimation();
                    break;
                }
            case WeaponObjectAnimationManager.weaponAnimation.ReloadIn:
                {
                    PlayReloadIn();
                    break;
                }
            case WeaponObjectAnimationManager.weaponAnimation.ReloadOut:
                {
                    PlayReloadOut();
                    break;
                }
        }
    }

    void PlayFireAnimation()
    {
        weapon.weaponAnimation.PlayerWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.Fire);
        // play fire sound
        SoundPlayer.SoundPlayerInstance.PlaySound(weapon.weaponStats.FireSound, transform.position, 1.0f);
        // play muzzle flash particle
        ParticleSystem muzzleflashInstance = Instantiate(MuzzleFlash, MuzzlePosition.position, weapon.ViewmodelCamera.transform.rotation);
        muzzleflashInstance.transform.parent = weapon.ViewmodelCamera.transform;
        // play recoil here
        PlayFireCameraRecoil();
    }

    void PlayFireCameraRecoil()
    {
        // Get vector that has a randomized recoil going upwards and sideways
        Func<float, float> randomRange = range => Random.Range(-range, range);
        Vector3 recoilVector = new Vector3(weapon.weaponStats.RecoilAnglePerShot, randomRange(weapon.weaponStats.RecoilAnglePerShot), 0) * -1;

        // get the camerarecoil component and add recoil
        weapon.playerAttack.pm.look.CameraRecoil.AddRecoil(recoilVector,
            weapon.weaponStats.RecoilTimePerShot,
            weapon.weaponStats.RecoilTimePerShot * _recoilRecoverMultiplier,
            weapon.weaponStats.RecoilMaxAngle
            );
    }

    public void CreateBulletHoleDecal(RaycastHit hit)
    {
        // play bullet hit particle at the hit position
        ParticleSystem bulletHitParticleInstance = Instantiate(HitParticle, hit.point, Quaternion.Euler(hit.normal));

        // spawn bullet decal that decays overtime
        GameObject bulletholeDecal = Instantiate(BulletHoleDecal, hit.point + (hit.normal * 0.01f), Quaternion.FromToRotation(Vector3.back, hit.normal));
        Destroy(bulletholeDecal, 10f);
    }

    void PlayReloadIn()
    {
        weapon.weaponAnimation.PlayerWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.ReloadIn);
    }

    void PlayReloadOut()
    {
        weapon.weaponAnimation.PlayerWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.ReloadOut);
        SoundPlayer.SoundPlayerInstance.PlaySound(weapon.weaponStats.ReloadSound, transform.position, 1.0f);
    }
}
