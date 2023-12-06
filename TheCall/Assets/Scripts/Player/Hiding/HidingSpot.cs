using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public GameObject player;
    public GameObject spot;
    public GameObject hiddenTeleport;
    public GameObject outsideTeleport;
    public Collider playerCollider;
    public Collider spotCollider;
    public AudioClip hidingInteractSound;
    public AudioSource hidingSpeaker;
    public AudioListener playerEars;
    public int hidingSpotIndex = 0;
    public bool spotOccupied = false;

    private HideController hidingController;
    private GameObject playerWrapperRef;

    public void PlayInteractSound()
    {
        hidingSpeaker.clip = hidingInteractSound;
        hidingSpeaker.Play();
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerWrapperRef = GameObject.Find("Player Wrapper");
        hidingController = playerWrapperRef.GetComponentInChildren<HideController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "player")
        {
            if (playerCollider.enabled == false)
            {
                playerCollider.enabled = true;
            }
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.hsInteract = this;
            Debug.Log("player in range");
            hidingController.currentSpotIndex = hidingSpotIndex;
            hidingController.canHide = true;
            GameManager.Instance.atHidingSpot = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "player")
        {
            Debug.Log("player no longer in range");
            hidingController.canHide = false;
            GameManager.Instance.atHidingSpot = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
