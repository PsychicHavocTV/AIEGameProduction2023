using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSoundEvent : MonoBehaviour
{
    public AudioSource audioSpeaker;

    public void PlaySound(AudioClip clip)
    {
        audioSpeaker.clip = clip;
        audioSpeaker.Play();
    }
}
