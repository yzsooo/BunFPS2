using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUDTimer : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // set the timer text with 0.00 format
    public void SetTime(float currentTime)
    {
        text.text = currentTime.ToString("0.00");
    }
}
