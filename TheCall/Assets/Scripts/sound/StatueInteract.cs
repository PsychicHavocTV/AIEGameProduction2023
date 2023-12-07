using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueInteract : MonoBehaviour
{
    public AudioListener playerEars;
    public AudioSource statueSpeaker;
    public AudioClip statueInteractSound;

    bool soundPlayed = false;

    public void PlayInteractSound()
    {
        Debug.Log("PlayInteractSound function called.");
        if (statueSpeaker.isPlaying == false)
        {
            Debug.Log("Statue interact sound playing.");
            statueSpeaker.clip = statueInteractSound;
            statueSpeaker.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.interactWithStatue == true)
        //{
        //    //if (soundPlayed == false)
        //    //{
        //        soundPlayed = true;
        //        PlayInteractSound();
        //    //}
        //}
        //else if (GameManager.Instance.interactWithStatue == false)
        //{
        //    if (soundPlayed == true)
        //    {
        //        soundPlayed = false;
        //    }
        //}
    }
}
