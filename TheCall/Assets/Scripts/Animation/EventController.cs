using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

public class EventController : MonoBehaviour
{
    [SerializeField]
    private string parameterName;
    private bool animPlaying = false;
    private bool particlePlaying = false;
    public bool animFinished = false;
    public bool hasSound = false;
    public Animator targetAnimator;
    public AnimationClip animClip;
    public Collider eventCollider;
    public AudioClip eventSound;
    public AudioSource eventSpeaker;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "player")
        {
            if (animPlaying == false)
            {
                if (hasSound == true)
                {
                    eventSpeaker.clip = eventSound;
                    if (eventSpeaker.isPlaying == false)
                    {
                        eventSpeaker.Play();
                    }
                }
                ExecuteEvent();
            }
        }
    }

    // Execute the event animation.
    private void ExecuteEvent()
    {
        animPlaying = true;
        targetAnimator.SetTrigger(parameterName);
        
        StartCoroutine(WaitForClip());
    }

    // Runs at the same time as the animation (Executed in 'ExecuteEvent'). Waits for the animation to finish, and then changes the 'animFinished' bool to match.
    private IEnumerator WaitForClip()
    {
        yield return new WaitForSeconds(animClip.length);
        animFinished = true;
        StopCoroutine(WaitForClip());
    }

    // Update is called once per frame
    void Update()
    {
        if (animFinished == true)
        {
            if (eventCollider.enabled == true)
            {
                eventCollider.enabled = false;
            }
            //if (targetAnimator.enabled == true)
            //{
            //    targetAnimator.enabled = false;
            //}
        }
    }
}
