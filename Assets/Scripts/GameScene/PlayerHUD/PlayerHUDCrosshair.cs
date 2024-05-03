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

    // based on the current weapon's max spread change the max spread of the crosshair
    public void SetCrosshairMaxSize(float maxWeaponSpreadAngle)
    {
        _crosshairRectMaxSize = Vector2.one * CrosshairCanvasMaxHeight * maxWeaponSpreadAngle;
    }

    // set the size of the crosshair based on the previously set base and max size
    public void SetCrosshairSize(float weaponSpreadInterpolate)
    {
        _crosshairRect.sizeDelta = Vector2.Lerp(_crosshairRectBaseSize, _crosshairRectMaxSize, weaponSpreadInterpolate);
    }
}
