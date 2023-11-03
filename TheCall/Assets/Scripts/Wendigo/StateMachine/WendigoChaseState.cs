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
        nma.speed = 10.5f;
        Debug.Log("CHASING!!");
    }

    public override void UpdateState(WendigoStateManager wendigo)
    {
        RaycastHit hit;
        playerDistance = Vector3.Distance(wendigo.playerRef.transform.position, wendigo.transform.position);

        //if (Physics.Raycast(wendigo.RaycastOrigin.transform.position, wendigo.RaycastOrigin.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        //{
            //Debug.Log(hit.transform.tag);
            //if (hit.transform.gameObject.tag == "Player")
            //{
                nma.ResetPath();
                nma.SetDestination(wendigo.playerRef.transform.position);
            //}
        //}
        if (!Physics.Raycast(wendigo.RaycastOrigin.transform.position, wendigo.RaycastOrigin.transform.TransformDirection(Vector3.forward),
            out hit, Mathf.Infinity) || hit.transform.gameObject.tag != "Player" )
        {
            Debug.Log(playerDistance);
            if (playerDistance >= 35)
            {
                if (wendigo.timerRunning == false)
                {
                    wendigo.ControlTimer();
                }
            }
        }
    }
}
