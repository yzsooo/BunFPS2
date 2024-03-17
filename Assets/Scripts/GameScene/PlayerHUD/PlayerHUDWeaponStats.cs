using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHUDWeaponStats : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI _ammoCount;
    [SerializeField]
    RectTransform _ammoBar;
    float baseAmmoBarWidth;
    public float AmmoBarPerShotLerp = 0.5f;

    float _maxAmmo;
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

    void SetAmmoBar(int ammo)
    {
        _ammoCount.text = Convert.ToString(ammo);
        // animate ammo bar with lerp
        float newWidth = Mathf.Lerp(0, baseAmmoBarWidth, Convert.ToSingle(ammo) / _maxAmmo);
        StartCoroutine(LerpAnimateAmmoBar(newWidth));
    }

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
}
