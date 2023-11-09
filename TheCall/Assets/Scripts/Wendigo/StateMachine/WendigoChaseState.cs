using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class WendigoChaseState : BaseState
{
    public NavMeshAgent nma;
    float playerDistance;

    public override void EnterState(WendigoStateManager wendigo)
    {
        nma.speed = 10f;
        Debug.Log("CHASING!!");
    }

    public override void UpdateState(WendigoStateManager wendigo)
    {
        RaycastHit hit;
        playerDistance = Vector3.Distance(wendigo.playerRef.transform.position, wendigo.transform.position);

        nma.ResetPath();
        nma.SetDestination(wendigo.playerRef.transform.position);
        Vector3 rayDirection = wendigo.playerRef.transform.position - wendigo.transform.position;

        // Check if the player is in front of the Wendigo & within its Field Of View.
         if ((Vector3.Angle(rayDirection, wendigo.transform.forward)) < 25) 
         {
             if ((Vector3.Angle(rayDirection, wendigo.transform.forward)) < 25) // Is player within field of view
             {
                if (Physics.Raycast(wendigo.transform.position, rayDirection, out hit, 30))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        //Debug.Log("player in front");
                        wendigo.StartChasing();
                    }
                    else
                    {
                        Debug.Log(playerDistance);
                        if (wendigo.timerRunning == false)
                        {
                            wendigo.ControlTimer();
                        }
                    }
                }
                else
                {
                    Debug.Log(playerDistance);
                    if (wendigo.timerRunning == false)
                    {
                        wendigo.ControlTimer();
                    }
                }
             }
         }
    }
}
