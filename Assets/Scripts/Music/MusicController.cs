using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public AudioClip[] audioClip;
    private AudioSource audioSource;
    private int indexAudio = 0;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //Verifica se existe algum duplicado, se sim então exclui
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

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

    private void PlayNextClip()
    {
        if (indexAudio == audioClip.Length)
            indexAudio = 0;

        Debug.Log("Tocando música do index: " + indexAudio);
        audioSource.PlayOneShot(audioClip[indexAudio]);
    }
}
