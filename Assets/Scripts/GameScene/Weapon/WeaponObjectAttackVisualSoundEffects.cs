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
    AudioClip _fireSound , _reloadSound;

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

    void PlayReloadIn()
    {
        weapon.weaponAnimation.PlayerWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.ReloadIn);
    }

    void PlayReloadOut()
    {
        weapon.weaponAnimation.PlayerWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.ReloadOut);
        SoundPlayer.SoundPlayerInstance.PlaySound(_reloadSound, transform.position, 1.0f);
    }
}
