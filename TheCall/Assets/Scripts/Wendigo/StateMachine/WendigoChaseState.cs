using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class WendigoChaseState : BaseState
{
    public NavMeshAgent nma;
    float playerDistance;
    Vector3 destinationPosition;

    public override void EnterState(WendigoStateManager wendigo)
    {
        GameManager.Instance.wendigoChasing = true;
        Debug.Log("Chasing...");
        nma.speed = 13.5f;
    }

    public override void UpdateState(WendigoStateManager wendigo)
    {
        if (GameManager.Instance.GameOver == false)
        {

            if (wendigo.hidingController.isHidden == true)
            {
                if (wendigo.timerRunning == false)
                {
                    wendigo.ControlTimer();
                }
            }

            if (wendigo.hidingController.isHidden == false)
            {
                RaycastHit hit;
                playerDistance = Vector3.Distance(wendigo.playerRef.transform.position, wendigo.transform.position);
                Vector3 rayDirection = wendigo.playerRef.transform.position - wendigo.transform.position;
                // Check if the player is in front of the Wendigo & within its Field Of View.
                if ((Vector3.Angle(rayDirection, wendigo.transform.forward)) < 45) 
                {
                    if ((Vector3.Angle(rayDirection, wendigo.transform.forward)) < 45) // Is player within field of view
                    {
                       if (Physics.Raycast(wendigo.transform.position, rayDirection, out hit, 90))
                       {
                           if (hit.collider.gameObject.tag == "Player")
                           {
                                //Debug.Log("player in front");
                               //if (wendigo.timerRunning == false)
                               //{
                               //    wendigo.ControlTimer();
                               //}
                               wendigo.StartChasing();
                           }
                           else
                           {
                               if (wendigo.timerRunning == false)
                               {
                                   wendigo.ControlTimer();
                               }
                           }
                       }
                       else
                       {
                           if (wendigo.timerRunning == false)
                           {
                               wendigo.ControlTimer();
                           }
                       }
                    }
                    else
                    {
                        if (wendigo.timerRunning == false)
                        {
                            wendigo.ControlTimer();
                        }
                    }
                }
                else
                {
                    if (wendigo.timerRunning == false)
                    {
                        wendigo.ControlTimer();
                    }
                }
            }

            nma.ResetPath();
            var path = new NavMeshPath();
            destinationPosition = wendigo.playerRef.transform.position;
            nma.CalculatePath(destinationPosition, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                nma.SetPath(path);
                nma.SetDestination(destinationPosition);
            }
            //else if (path.status == NavMeshPathStatus.PathInvalid)
            //{
            //  nma.SetDestination(destinationPosition);
            //}
        }
    }
}
