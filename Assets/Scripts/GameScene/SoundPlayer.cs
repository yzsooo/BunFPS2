using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    
    public static SoundPlayer SoundPlayerInstance { get; private set; }

    private void Awake()
    {
        SetSoundPlayerInstance();
    }

    void SetSoundPlayerInstance()
    {
        // check if SPI already exists and do nothing if it is
        if (SoundPlayerInstance != null)
        {
            return;
        }
        // set SPI as this object
        SoundPlayerInstance = this;
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volume)
    {
        // Create a empty gameobject with audiosource and soundplayerobject
        SoundPlayerObject spInstance = new GameObject().AddComponent<AudioSource>().AddComponent<SoundPlayerObject>();
        // play the sound by passing the audioclip and position it should play on to the new instance
        spInstance.PlaySound(audioClip, position, volume);
    }
}
