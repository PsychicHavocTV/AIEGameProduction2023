using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WendigoJumpscareState : BaseState
{
    public NavMeshAgent nma;

    public override void EnterState(WendigoStateManager wendigo)
    {
        Debug.Log("waiting behind player...");
    }

    public override void UpdateState(WendigoStateManager wendigo)
    {
        BehindPlayerScare(wendigo);
    }

    public void BehindPlayerScare(WendigoStateManager wendigo)
    {
        if (wendigo.goBehindPlayer == false)
        {
            nma.enabled = false;
            wendigo.behindPlayerSpawn.transform.forward = wendigo.playerRef.transform.forward;
            wendigo.transform.position = new Vector3(wendigo.behindPlayerSpawn.transform.position.x, wendigo.playerRef.transform.position.y + 0.5f,
                                                     wendigo.behindPlayerSpawn.transform.position.z);
            wendigo.transform.forward = wendigo.behindPlayerSpawn.transform.forward;
            wendigo.goBehindPlayer = true;
            wendigo.isBehindPlayer = true;
            nma.enabled = true;
        }
        if (wendigo.teleportAway == true)
        {
            wendigo.teleportAway = false;
            TeleportAway(wendigo);
        }
    }

    public void TeleportAway(WendigoStateManager wendigo)
    {
        //if (wendigo.isBehindPlayer == false)
        //{
            wendigo.isBehindPlayer = false;
            // Actually teleport the Wendigo away. will disable wendigo for now.
            wendigo.transform.position = new Vector3(100, 100, 100);
        //}    
    }
}
