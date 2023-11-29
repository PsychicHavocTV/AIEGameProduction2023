using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TeleportTrigger : MonoBehaviour
{
    public GameObject activeWendigo;
    public GameObject[] wendigos;
    public NavMeshAgent[] wendigoNMAs;
    public WendigoStateManager[] wendigoControllers;
    public GameObject[] teleportTriggers;
    public bool triggered = false;
    public bool triggersDisabled = false;

    public void DisableTriggers()
    {
        for (int i = 0; i <= teleportTriggers.Length; i++)
        {
            teleportTriggers[i].SetActive(false);
        }
        triggersDisabled = true;
    }

    public void EnableTriggers()
    {
        for (int i = 0; i <= teleportTriggers.Length; i++)
        {
            teleportTriggers[i].SetActive(true);
        }
        triggersDisabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" || other.tag == "Player")
        {
            ActivateWendigo();
            Debug.Log("Wendigo Ready To Teleport.");
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
                            wendigos[0].SetActive(false);
                            GameManager.Instance.currentWendigo = 1;
                            wendigos[1].SetActive(true);
                            activeWendigo = wendigos[1];
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
                                wendigos[1].SetActive(false);
                                GameManager.Instance.currentWendigo = 0;
                                wendigos[0].SetActive(true);
                                activeWendigo = wendigos[0];
                            }
                            triggered = false;
                        }
                        break;
                    }
            }
        }
        else
        {
            return;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
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
        if (GameManager.Instance.wendigoChasing == true)
        {
            if (triggersDisabled == false)
            {
                DisableTriggers();
            }
        }
    }
}
