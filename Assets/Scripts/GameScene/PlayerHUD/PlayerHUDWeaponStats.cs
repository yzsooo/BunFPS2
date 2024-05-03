using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.Mathematics;

public class PlayerHUDWeaponStats : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI _ammoCount;
    [SerializeField]
    RectTransform _ammoBar;
    float baseAmmoBarWidth;
    public float AmmoBarPerShotLerp = 0.5f;

    float _maxAmmo;

    enum AmmoBarStatus { Normal, Reloading };
    AmmoBarStatus _currentAmmoBarStatus = AmmoBarStatus.Normal;

    public int MaxAmmo
    {
        set { _maxAmmo = value; }
    }

    public int AmmoCount
    {
        set { SetAmmoBar(value); }
    }

    private void Awake()
    {
        baseAmmoBarWidth = _ammoBar.sizeDelta.x;
    }

    // set the ammo count on hud and resize the ammo bar based on max ammo count with coroutine animation
    void SetAmmoBar(int ammo)
    {
        // if weapon is reloading dont update ammo count
        if (_currentAmmoBarStatus == AmmoBarStatus.Reloading) { return; }
        
        // update ammo count text and animate ammo bar with lerp
        _ammoCount.text = Convert.ToString(ammo);
        float newWidth = Mathf.Lerp(0, baseAmmoBarWidth, Convert.ToSingle(ammo) / _maxAmmo);
        if (!gameObject.activeSelf) { return; }
        StartCoroutine(LerpAnimateAmmoBar(newWidth));
    }

    // resize ammo bar to set width overtime
    IEnumerator LerpAnimateAmmoBar(float newWidth)
    {
        float t = AmmoBarPerShotLerp;
        Vector2 baseSizeDelta = _ammoBar.sizeDelta;
        while (t > 0.0)
        {
            t -= Time.deltaTime;
            Vector2 lerpSizeDelta = baseSizeDelta;
            lerpSizeDelta.x = Mathf.Lerp(newWidth, baseSizeDelta.x, t);
            _ammoBar.sizeDelta = lerpSizeDelta;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    // Refill the ammo bar based on weapon's reloadInterp (the reload time of the weapons)
    public void ReloadAmmoBar(float reloadInterp)
    {
        // While the weapon is reloading interpolate the ammo bar based on the reload time
        _currentAmmoBarStatus = AmmoBarStatus.Reloading;
        Vector2 lerpSizeDelta = _ammoBar.sizeDelta;
        lerpSizeDelta.x = Mathf.Lerp(0, baseAmmoBarWidth, reloadInterp);
        _ammoBar.sizeDelta = lerpSizeDelta;
        
        // exit once weapon is not reloading
        if (reloadInterp >= 1)
        {
            _currentAmmoBarStatus = AmmoBarStatus.Normal;
        }
    }
}
