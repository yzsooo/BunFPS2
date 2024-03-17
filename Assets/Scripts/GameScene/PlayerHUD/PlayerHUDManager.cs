using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{

    [Header("HUD Components")]
    public PlayerHUDCrosshair HUDCrosshair;

    public void Awake()
    {
        HUDCrosshair.CrosshairCanvasMaxHeight = GetComponent<RectTransform>().sizeDelta.y;    
    }
}
