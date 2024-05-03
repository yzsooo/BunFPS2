using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerObject : MonoBehaviour
{
    private AudioSource audiosource;
    private bool bActivated = false;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // destroy the gameobject when sound has finished playing
    private void Update()
    {
        if (!audiosource.isPlaying && bActivated)
        {
            Destroy(gameObject);
        }
    }

    // Called from SoundPlayer
    // move this object to the set location and play the given audioclip
    public void PlaySound(AudioClip ac, Vector3 position, float volume)
    {
        transform.position = position;

        audiosource.clip = ac;
        audiosource.volume = volume;

        bActivated = true;

        audiosource.Play();
    }
}
