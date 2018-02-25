using jamtasticvol3.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : Singleton<MusicController>
{
    public AudioClip[] audioClip;
    private AudioSource audioSource;
    private int indexAudio = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClip.Length > 0)
            PlayNextClip();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            indexAudio += 1;
            PlayNextClip();
        }
    }

    public void ToggleMusic(bool toggle)
    {
        audioSource.enabled = toggle;
    }

    private void PlayNextClip()
    {
        if (indexAudio == audioClip.Length)
            indexAudio = 0;

        Debug.Log("Tocando música do index: " + indexAudio);
        audioSource.PlayOneShot(audioClip[indexAudio]);
    }
}
