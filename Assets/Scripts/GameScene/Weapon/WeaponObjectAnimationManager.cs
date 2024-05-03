using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectAnimationManager : MonoBehaviour
{
    public WeaponObject weapon;

    public Animator[] Animators;
    public enum weaponAnimation
    {
        Idle,
        Fire,
        ReloadIn,
        ReloadOut
    }

    // play the animation in anim variabld
    public void PlayerWeaponAnimation(weaponAnimation anim = weaponAnimation.Idle)
    {
        ResetAnimationTrigger();
        switch (anim)
        {
            default:
                break;
            case weaponAnimation.Fire:
                foreach (Animator a in Animators)
                {
                    a.SetTrigger("Fire");
                }
                break;
            case weaponAnimation.ReloadIn:
                foreach (Animator a in Animators)
                {
                    a.SetBool("Reload", true);
                }
                break;
            case weaponAnimation.ReloadOut:
                foreach (Animator a in Animators)
                {
                    a.SetBool("Reload", false);
                }
                break;
        }
    }

    // reset animation trigger
    void ResetAnimationTrigger()
    {
        foreach (Animator a in Animators)
        {
            a.ResetTrigger("Fire");
            a.ResetTrigger("Reload");
        }
    }
}
