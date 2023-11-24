using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public GameObject spot;
    public GameObject hiddenTeleport;
    public GameObject outsideTeleport;
    public Collider spotCollider;
    public int hidingSpotIndex = 0;
    public bool spotOccupied = false;

    private HideController hidingController;
    private GameObject playerWrapperRef;
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
