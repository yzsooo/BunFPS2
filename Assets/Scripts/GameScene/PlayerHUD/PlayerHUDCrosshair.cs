using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDCrosshair : MonoBehaviour
{
    public float CrosshairCanvasMaxHeight;

    RectTransform _crosshairRect;

    Vector2 _crosshairRectBaseSize;
    Vector2 _crosshairRectMaxSize;

    private void Awake()
    {
        _crosshairRect = GetComponent<RectTransform>();
    }

    public void SetCrosshairMaxSize(float maxWeaponSpreadAngle)
    {
        // based on the current weapon's max spread change the max spread of the crosshair
        _crosshairRectMaxSize = Vector2.one * CrosshairCanvasMaxHeight * maxWeaponSpreadAngle;
    }

    public void SetCrosshairSize(float weaponSpreadInterpolate)
    {
        _crosshairRect.sizeDelta = Vector2.Lerp(_crosshairRectBaseSize, _crosshairRectMaxSize, weaponSpreadInterpolate);
    }
}
