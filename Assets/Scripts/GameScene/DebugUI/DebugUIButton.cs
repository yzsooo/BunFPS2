using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUIButton : MonoBehaviour
{
    RectTransform _submenu;

    // set _submenu variable of this button and disable it so its not visible
    private void Awake()
    {
        _submenu = GetComponentInChildren<RectTransform>().Find("Submenu").GetComponent<RectTransform>();

        if (!_submenu)
        {
            Debug.Log(name + "'s submenu is empty");
            return;
        }
        _submenu.gameObject.SetActive(false);
    }

    // Flip visibility of submenu
    public void ToggleSubmenu()
    {
        _submenu.gameObject.SetActive(!_submenu.gameObject.activeSelf);
    }
}
