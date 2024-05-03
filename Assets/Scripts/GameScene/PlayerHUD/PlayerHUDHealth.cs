using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUDHealth : MonoBehaviour
{
    public TextMeshProUGUI HPText;

    // Update health text based on input
    public void SetHealth(float newHealth)
    {
        HPText.text = Convert.ToString(newHealth);
    }
}
