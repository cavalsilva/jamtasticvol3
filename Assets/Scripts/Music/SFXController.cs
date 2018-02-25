using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

    public AudioClip audioClipClick;
    public AudioClip audioClipDrag;

    private AudioSource audioSource;

    private void Awake()
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
