using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;

public class WendigoStateManager : MonoBehaviour
{
    [SerializeField]
    GameObject Wendigo;
    [SerializeField]
    NavMeshAgent nma;
    public GameObject playerRef;
    public PlayerController pController;
    BaseState currentState;
    WendigoRoamingState roamingState = new WendigoRoamingState();

    private void Start()
    {
        // Starting state for the state machine.
        currentState = roamingState;
        roamingState.Wendigo = Wendigo;
        roamingState.nma = nma;

        pController = playerRef.GetComponent<PlayerController>();

        currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        currentState.UpdateState(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (currentState == roamingState)
        {
            if (other.tag != "Player" && other.tag != "Ground")
            {
                if (roamingState.moving == true)
                {
                    roamingState.moving = false;
                    roamingState.moveAmount = 0;
                }
            }
        }
    }
}
