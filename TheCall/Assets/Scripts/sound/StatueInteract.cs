using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueInteract : MonoBehaviour
{
    public AudioListener playerEars;
    public AudioSource statueSpeaker;
    public AudioClip statueInteractSound;

    public void PlayInteractSound()
    {
        if (statueSpeaker.isPlaying == false)
        {
            statueSpeaker.clip = statueInteractSound;
            statueSpeaker.Play();
        }
        return;
    }
}
