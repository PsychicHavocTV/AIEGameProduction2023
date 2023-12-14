using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    public GameObject playerBody;
    public PlayerController playerController;
    public CharacterController playerCollider;
    public HidingSpot[] hidingSpots;
    
    public int currentSpotIndex = 99;
    
    public bool canHide = false;
    public bool isHiding = false;
    public bool isHidden = false;
    public bool exitingHiding = false;

    private Vector3 m_prevPosition = Vector3.zero;

    public void EnterHidingSpot(HidingSpot spot)
    {
        Debug.Log("ENTERING HIDING SPOT");
        m_prevPosition = playerBody.transform.position;
        playerCollider.enabled = false;
        playerBody.transform.position = new Vector3(spot.hiddenTeleport.transform.position.x, spot.hiddenTeleport.transform.position.y, spot.hiddenTeleport.transform.position.z);
        playerCollider.enabled = true;
        playerController.canMove = false;
        if (exitingHiding == false && canHide == true)
        {
            playerCollider.detectCollisions = false;
        }
        return;
    }

    public void ExitHidingSpot(HidingSpot spot)
    {
        Debug.Log("EXITING HIDING SPOT");
        playerController.canMove = true;
        spot.spotCollider.enabled = false;
        playerCollider.detectCollisions = true;
        playerController.enabled = false;
        exitingHiding = true;
        playerBody.transform.position = m_prevPosition;
        playerController.enabled = true;
        spot.spotCollider.enabled = true;
        return;
    }
}
