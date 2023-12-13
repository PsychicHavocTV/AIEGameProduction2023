using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interaction))]
public class HidingSpot : MonoBehaviour
{
    public GameObject player;
    public GameObject spot;
    public GameObject hiddenTeleport;
    public Collider spotCollider;
    public AudioClip hidingInteractSound;
    public AudioSource hidingSpeaker;
    public AudioListener playerEars;
    public int hidingSpotIndex = 0;
    public bool spotOccupied = false;

    private HideController hidingController;

    private Interaction m_interaction;

    void Start()
    {
        m_interaction = transform.GetComponent<Interaction>();
        hidingController = player.GetComponentInChildren<HideController>();
    }

    void Update()
    {
        if (m_interaction.Interactable)
        {
            Debug.Log("player in range");
            hidingController.currentSpotIndex = hidingSpotIndex;
            hidingController.canHide = true;

            if (m_interaction.Interacted)
            {
                PlayInteractSound();
                if (hidingController.isHidden == true || hidingController.isHiding == true)
                {
                    hidingController.exitingHiding = true;
                    hidingController.ExitHidingSpot(hidingController.hidingSpots[hidingController.currentSpotIndex]);
                    hidingController.hidingSpots[hidingController.currentSpotIndex].spotOccupied = false;
                    hidingController.isHidden = false;
                    hidingController.isHiding = false;
                    hidingController.exitingHiding = false;
                    //hidingController.hidingSpots[hidingController.currentSpotIndex].hidingSpotIndex = 99;
                    Debug.Log("Player is no longer hiding.");
                }
                else if (hidingController.canHide == true)
                {
                    if (hidingController.isHiding == false && hidingController.isHidden == false && hidingController.exitingHiding == false)
                    {
                        hidingController.isHiding = true;
                        hidingController.EnterHidingSpot(hidingController.hidingSpots[hidingController.currentSpotIndex]);
                        hidingController.hidingSpots[hidingController.currentSpotIndex].spotOccupied = true;
                        hidingController.isHiding = false;
                        hidingController.isHidden = true;
                        Debug.Log("Player is hiding.");
                    }
                }
            }
        }
        else
        {
            Debug.Log("player no longer in range");
            hidingController.canHide = false;
        }
    }

    public void PlayInteractSound()
    {
        hidingSpeaker.clip = hidingInteractSound;
        hidingSpeaker.Play();
    }
}
