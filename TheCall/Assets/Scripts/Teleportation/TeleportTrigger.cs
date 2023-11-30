using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TeleportTrigger : MonoBehaviour
{
    public Teleportation teleportWendigo;
    public TeleportTrigger duoTrigger;
    public TeleportTrigger[] teleportTriggers;
    public GameObject activeWendigo;
    public GameObject[] teleportTriggersGO;
    public GameObject[] wendigos;
    public NavMeshAgent[] wendigoNMAs;
    public WendigoStateManager[] wendigoControllers;
    public bool triggered = false;
    public bool hasDuo = false;
    public int triggerIndex;
    public int previousIndex;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player" || other.tag == "Player")
        {
            if (triggered == true)
            {
                triggered = false;
                if (hasDuo == true)
                {
                    duoTrigger.triggered = false;
                }
            }
            else if (triggered == false)
            {
                triggered = true;
                if (hasDuo == true)
                {
                    duoTrigger.triggered = true;
                }
            }

            //triggered = !triggered;
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

    public void ActivateWendigo()
    {
        
        //if (GameManager.Instance.wendigoChasing == false)
        //{
            teleportWendigo.TeleportToLocation(activeWendigo.GetComponent<WendigoStateManager>());
            for (int i = 0; i <= wendigos.Length - 1; i++)
            {
                wendigos[i].SetActive(false);
            }

            if (triggered == false || duoTrigger.triggered == false)
            {
                wendigos[previousIndex].SetActive(true);
                activeWendigo = wendigos[previousIndex];
                GameManager.Instance.currentWendigo = previousIndex;
            }
            else if (triggered == true)
            {
                wendigos[triggerIndex].SetActive(true);
                activeWendigo = wendigos[triggerIndex];
                GameManager.Instance.currentWendigo = triggerIndex;
            }

            for (int i = 0; i <= teleportTriggers.Length - 1; i++)
            {
                if (teleportTriggers[i].activeWendigo != activeWendigo)
                {
                    teleportTriggers[i].activeWendigo = activeWendigo;
                }
            }

            //switch(GameManager.Instance.currentWendigo)
            //{
            //    case 0:
            //        {
            //            // Deactivate Wendigo[0] and activate Wendigo[1].
            //            if (wendigos[0].activeSelf == true)
            //            {
            //                wendigos[0].SetActive(false);
            //                GameManager.Instance.currentWendigo = 1;
            //                wendigos[1].SetActive(true);
            //                activeWendigo = wendigos[1];
            //                teleportWendigo = activeWendigo.GetComponent<Teleportation>();
            //                wendigoControllers[1].StartRoaming();
            //            }
            //            triggered = true;
            //            break;
            //        }
            //    case 1:
            //        {
            //            if (triggered == false)
            //            {
            //                // Deactivate Wendigo[1] and activate Wendigo[2].
            //            }
            //            else if (triggered == true)
            //            {
            //                // Deactivate Wendigo[1] and activate Wendigo[0].
            //                if (wendigos[1].activeSelf == true)
            //                {
            //                    teleportWendigo.TeleportToLocation(activeWendigo.GetComponent<WendigoStateManager>());
            //                    wendigos[1].SetActive(false);
            //                    GameManager.Instance.currentWendigo = 0;
            //                    wendigos[0].SetActive(true);
            //                    activeWendigo = wendigos[0];
            //                    teleportWendigo = activeWendigo.GetComponent<Teleportation>();
            //                    wendigoControllers[0].StartRoaming();
            //                }
            //                triggered = false;
            //            }
            //            break;
            //        }
            //}
        //a}
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
            //if (GameManager.Instance.outOfPlayerView == true)
            //{
                ActivateWendigo();
            //}
        }
        if (GameManager.Instance.activeWendigo != activeWendigo)
        {
            GameManager.Instance.activeWendigo = activeWendigo;
        }
    }
}
