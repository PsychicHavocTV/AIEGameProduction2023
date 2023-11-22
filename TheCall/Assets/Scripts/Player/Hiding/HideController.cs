using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    
    public HidingSpot[] hidingSpots;
    
    public int currentSpotIndex = 99;
    
    public bool canHide = false;
    public bool isHiding = false;
    public bool isHidden = false;

    // Update is called once per frame
    void Update()
    {
        
    }
}
