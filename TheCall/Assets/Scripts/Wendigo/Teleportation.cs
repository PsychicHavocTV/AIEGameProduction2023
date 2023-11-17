using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleportation : MonoBehaviour
{
    public GameObject[] teleportLocations;
    public NavMeshAgent nma;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportToLocation(WendigoStateManager wendigo)
    {
        nma.enabled = false;
        int locationChoice = 0;
        locationChoice = Random.Range(1, teleportLocations.Length);
        GameObject location = teleportLocations[locationChoice - 1];
        wendigo.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, location.transform.position.z);
        
        nma.enabled = true;
        //wendigo.StartRoaming();
    }
}
