using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    public GameObject playerBody;
    public PlayerController playerController;
    public Collider playerCollider;
    public HidingSpot[] hidingSpots;
    public Transform hidingPosition;
    
    public int currentSpotIndex = 99;
    
    public bool canHide = false;
    public bool isHiding = false;
    public bool isHidden = false;
    public bool exitingHiding = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterHidingSpot(HidingSpot spot)
    {
        Debug.Log("ENTERING HIDING SPOT");
        hidingPosition.transform.position = playerBody.transform.position;
        playerBody.transform.position = new Vector3(spot.hiddenTeleport.transform.position.x, spot.hiddenTeleport.transform.position.y, spot.hiddenTeleport.transform.position.z);
        if (exitingHiding == false && canHide == true)
        {
            if (playerCollider.enabled == true)
            {
                playerCollider.enabled = false;
            }
        }
    }

    public void ExitHidingSpot(HidingSpot spot)
    {
        Debug.Log("EXITING HIDING SPOT");
        spot.spotCollider.enabled = false;
        if (playerCollider.enabled == false)
        {
            playerCollider.enabled = true;
        }
        playerController.enabled = false;
        exitingHiding = true;
        playerBody.transform.position = new Vector3(0, 0, 0);
        playerController.enabled = true;
        spot.spotCollider.enabled = true;
    }
}
