using jamtasticvol3.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : Singleton<SFXController>
{
    public AudioClip audioClipClick;
    public AudioClip audioClipDrag;

    AudioSource _audioSource;
    private AudioSource audioSource
    {
        get
        {
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
                _audioSource.loop = false;
                _audioSource.playOnAwake = false;
            }

            return _audioSource;
        }

        set { _audioSource = value; }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundClick()
    {
        Debug.Log("Tocou Som Click");
        audioSource.clip = audioClipClick;
        audioSource.Play();
    }

    public void PlaySoundDrag()
    {
        Debug.Log("Tocou Som Drag");
        audioSource.clip = audioClipDrag;
        audioSource.Play();
    }
}
