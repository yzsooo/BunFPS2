using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamageVisualEffects : MonoBehaviour
{
    public PlayerManager pm;
    [Header("On Damage Recoil")]
    public float onDamageRecoilAngle;
    public float onDamageRecoilDuration;
    [Header("Full screen flash")]
    public Animator animation;

    // add recoil to the camera and play the animation when taking damage
    public void TakeDamageVisualEffects()
    {
        pm.look.CameraRecoil.AddRecoil(Vector3.left * onDamageRecoilAngle, onDamageRecoilDuration, onDamageRecoilDuration, onDamageRecoilAngle);
        animation.ResetTrigger("TakeDamage");
        animation.SetTrigger("TakeDamage");
    }
}
