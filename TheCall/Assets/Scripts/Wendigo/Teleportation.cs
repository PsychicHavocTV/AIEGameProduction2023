using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleportation : MonoBehaviour
{
    public GameObject[] teleportLocations;
    public NavMeshAgent nma;
    public int locationChoice = 0;
    public bool hasTeleported = false;

    public void TeleportToNext(WendigoStateManager wendigo, Vector3 position)
    {
        nma.enabled = false;
        wendigo.transform.position = position;
        nma.enabled = true;
        hasTeleported = true;
    }

    public void TeleportToLocation(WendigoStateManager wendigo)
    {
        if (teleportLocations.Length > 0)
        {
            nma.enabled = false;
            locationChoice = Random.Range(1, teleportLocations.Length);
            GameObject location = teleportLocations[locationChoice - 1];
            wendigo.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, location.transform.position.z);
            
        }
        nma.enabled = true;
        hasTeleported = true;
    }
}
