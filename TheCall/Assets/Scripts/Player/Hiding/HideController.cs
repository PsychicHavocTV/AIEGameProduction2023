using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    public GameObject playerBody;
    public PlayerController playerController;
    public Collider playerCollider;
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
        playerController.enabled = false;
        exitingHiding = true;
        playerBody.transform.position = m_prevPosition;
        if (playerCollider.enabled == false)
        {
            playerCollider.enabled = true;
        }
        playerController.enabled = true;
        spot.spotCollider.enabled = true;
    }
}
