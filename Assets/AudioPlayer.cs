using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Range(0,3f)] public float randomPitch = 0.2f;

    private AudioSource _audioSource;

    public AudioSource audioSource
    {
        get 
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();         
            return _audioSource;
        }
        set { _audioSource = value; }
    }

    public void PlayRandomPitch(AudioClip clip)
    {
        audioSource.pitch = 1 + Random.Range(-randomPitch, randomPitch);
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
