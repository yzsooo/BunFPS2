using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{

    public PlayerHUDManager HUD;

    public float currentTime;
    public bool timerActive;

    private void Update()
    {
        UpdateTimer();
    }

    // Update HUD's timer if the timer is active
    void UpdateTimer()
    {
        // if timer is inactive disable text on HUD
        if (!timerActive)
        {
            HUD.HUDTimer.gameObject.SetActive(false);
        }
        // if timer is active increment timer and display on HUD
        if (timerActive)
        {
            HUD.HUDTimer.gameObject.SetActive(true);
            currentTime += Time.deltaTime;
            HUD.HUDTimer.SetTime(currentTime);
        }
    }
}
