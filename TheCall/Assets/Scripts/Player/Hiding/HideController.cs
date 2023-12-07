using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    public GameObject playerBody;
    public PlayerController playerController;
    public CharacterController playerCollider;
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
        playerCollider.enabled = false;
        playerBody.transform.position = new Vector3(spot.hiddenTeleport.transform.position.x, spot.hiddenTeleport.transform.position.y, spot.hiddenTeleport.transform.position.z);
        playerCollider.enabled = true;
        playerController.canMove = false;
        if (exitingHiding == false && canHide == true)
        {
            playerCollider.detectCollisions = false;
        }
    }

    public void ExitHidingSpot(HidingSpot spot)
    {
        Debug.Log("EXITING HIDING SPOT");
        playerController.canMove = true;
        spot.spotCollider.enabled = false;
        playerCollider.detectCollisions = true;
        playerController.enabled = false;
        exitingHiding = true;
        playerBody.transform.position = new Vector3(0, 0, 0);
        playerController.enabled = true;
        spot.spotCollider.enabled = true;
    }
}
