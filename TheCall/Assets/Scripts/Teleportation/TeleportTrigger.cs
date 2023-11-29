using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TeleportTrigger : MonoBehaviour
{
    public Teleportation teleportWendigo;
    public GameObject activeWendigo;
    public GameObject[] teleportTriggers;
    public GameObject[] wendigos;
    public NavMeshAgent[] wendigoNMAs;
    public WendigoStateManager[] wendigoControllers;
    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" || other.tag == "Player")
        {
            triggered = !triggered;
            if (GameManager.Instance.wendigoChasing == false)
            {
                ActivateWendigo();
                Debug.Log("Wendigo Ready To Teleport.");
            }
            else if (GameManager.Instance.wendigoChasing == true && teleportWendigo.hasTeleported == true)
            {
                teleportWendigo.hasTeleported = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player" || other.tag == "Player")
        {
        }
    }

    public void ActivateWendigo()
    {
        
        if (GameManager.Instance.wendigoChasing == false)
        {
            switch(GameManager.Instance.currentWendigo)
            {
                case 0:
                    {
                        // Deactivate Wendigo[0] and activate Wendigo[1].
                        if (wendigos[0].activeSelf == true)
                        {
                            teleportWendigo.TeleportToLocation(activeWendigo.GetComponent<WendigoStateManager>());
                            wendigos[0].SetActive(false);
                            GameManager.Instance.currentWendigo = 1;
                            wendigos[1].SetActive(true);
                            activeWendigo = wendigos[1];
                            teleportWendigo = activeWendigo.GetComponent<Teleportation>();
                            wendigoControllers[1].StartRoaming();
                        }
                        triggered = true;
                        break;
                    }
                case 1:
                    {
                        if (triggered == false)
                        {
                            // Deactivate Wendigo[1] and activate Wendigo[2].
                        }
                        else if (triggered == true)
                        {
                            // Deactivate Wendigo[1] and activate Wendigo[0].
                            if (wendigos[1].activeSelf == true)
                            {
                                teleportWendigo.TeleportToLocation(activeWendigo.GetComponent<WendigoStateManager>());
                                wendigos[1].SetActive(false);
                                GameManager.Instance.currentWendigo = 0;
                                wendigos[0].SetActive(true);
                                activeWendigo = wendigos[0];
                                teleportWendigo = activeWendigo.GetComponent<Teleportation>();
                                wendigoControllers[0].StartRoaming();
                            }
                            triggered = false;
                        }
                        break;
                    }
            }
        }
        return;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Set the default objects in the nav mesh agents and state machine controller arrays.
        activeWendigo = wendigos[0];
        for (int i = 0; i <= wendigos.Length - 1; i++)
        {
            wendigoNMAs[i] = wendigos[i].GetComponent<NavMeshAgent>();
            wendigoControllers[i] = wendigos[i].GetComponent<WendigoStateManager>();
            Debug.Log("NAV MESH AGENT #" + i + " STATUS: SET");
            Debug.Log("STATE MACHINE CONTROLLER #" + i + " STATUS: SET");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.finishedChasing == true && teleportWendigo.hasTeleported == false)
        {
            if (GameManager.Instance.outOfPlayerView == true)
            {
                ActivateWendigo();
            }
        }
    }
}
