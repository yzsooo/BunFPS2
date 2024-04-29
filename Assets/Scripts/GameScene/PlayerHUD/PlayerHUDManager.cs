using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{

    [Header("HUD Components")]
    public PlayerHUDCrosshair HUDCrosshair;
    public PlayerHUDWeaponStats HUDWeaponStats;
    public PlayerHUDHealth HUDHealth;
    public PlayerHUDTimer HUDTimer;
    public TextMeshProUGUI HUDMessage;
    [SerializeField]
    List<RectTransform> _HUDComponents = new List<RectTransform>();

    float _messageDecaySeconds = 5.0f;

    // HUD modes
    public enum PlayerHUDMode
    {
        Full,
        MessageOnly,
        Disabled
    }
    public PlayerHUDMode HUDMode;

    public void Awake()
    {
        SetupHUDComponentsList();
        HUDCrosshair.CrosshairCanvasMaxHeight = GetComponent<RectTransform>().sizeDelta.y;
        ChangeHUDMode(PlayerHUDMode.Disabled);
        ShowMessage("");
    }

    // set all hud components into a list
    void SetupHUDComponentsList()
    {
        foreach(RectTransform t in transform)
        {
            _HUDComponents.Add(t);
        }
    }

    // Set hud mode to;
    // Full: all hud components active
    // Disabled: all hud components inactive
    // MessageOnly: Message component active
    public void ChangeHUDMode(PlayerHUDMode newMode)
    {
        switch(newMode)
        {
            case PlayerHUDMode.Full:
                {
                    foreach (RectTransform t in _HUDComponents) { t.gameObject.SetActive(true); }
                    break;
                }
            case PlayerHUDMode.Disabled:
                {
                    foreach (RectTransform t in _HUDComponents) { t.gameObject.SetActive(false); }
                    break;
                }
            case PlayerHUDMode.MessageOnly:
                {
                    foreach (RectTransform t in _HUDComponents)
                    {
                        t.gameObject.SetActive(false);
                        if (t.name == HUDMessage.name)
                        {
                            t.gameObject.SetActive(true);
                        }
                    }
                    break;
                }
        }
    }

    // show a message on hud and disable it after a while if decay is enabled
    public void ShowMessage(string newMessage, bool bdecay = false)
    {
        Debug.Log(newMessage);
        HUDMessage.text = newMessage;
        if (bdecay) { StartCoroutine("DecayMessage"); }
    }

    // show a blank message after waiting _messageDecaySeconds seconds
    IEnumerator DecayMessage()
    {
        yield return new WaitForSeconds(_messageDecaySeconds);
        ShowMessage("");
        yield return null;
    }

}
