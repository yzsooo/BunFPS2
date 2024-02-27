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

    public void PlaySound(AudioClip ac, Vector3 position, float volume)
    {
        audiosource.volume = volume;
        bActivated = true;
        transform.position = position;
        audiosource.clip = ac;

        audiosource.Play();
    }
}
