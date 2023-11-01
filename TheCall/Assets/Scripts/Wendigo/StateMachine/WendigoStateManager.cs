using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class WendigoStateManager : MonoBehaviour
{
    [SerializeField]
    GameObject Wendigo;
    BaseState currentState;
    WendigoRoamingState roamingState = new WendigoRoamingState();

    private void Start()
    {
        // Starting state for the state machine.
        currentState = roamingState;
        roamingState.Wendigo = Wendigo;

        currentState.EnterState(this);
    }

    private void Update()
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
