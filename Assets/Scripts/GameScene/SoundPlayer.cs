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

    // Set soundplayer Singleton
    void SetSoundPlayerInstance()
    {
        if (SoundPlayerInstance) { return; }
        SoundPlayerInstance = this;
    }

    // Create a new gameobject at a set location and play a sound
    public void PlaySound(AudioClip audioClip, Vector3 position, float volume)
    {
        // Create a empty gameobject with audiosource and soundplayerobject
        SoundPlayerObject spInstance = new GameObject().AddComponent<AudioSource>().AddComponent<SoundPlayerObject>();
        // play the sound by passing the audioclip and position it should play on to the new instance
        spInstance.PlaySound(audioClip, position, volume);
    }
}
