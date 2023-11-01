using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WendigoRoamingState : BaseState
{
    int directionChoice = 0;
    public int moveAmount = 0;
    int roamSpeed = 3;
    public bool moving = false;
    public GameObject Wendigo;
    float oldX = 0;
    float oldZ = 0;
    float DestinationX = 0;
    float DestinationZ = 0;
    bool chanceCalculated = false;
    bool destinationSet = false;
    bool findingPlayer = false;
    int alertChance = 0;
    Vector3 destinationPosition;
    public NavMeshAgent nma;
    
    
    public override void EnterState(WendigoStateManager wendigo)
    {
        Debug.Log("Roaming State Entered..");
    }


    public override void UpdateState(WendigoStateManager wendigo)
    {
        Debug.Log(Vector3.Distance(Wendigo.transform.localPosition, wendigo.playerRef.transform.localPosition));

        //if (wendigo.pController.takingPhoto == true)
        //{
        //    chanceCalculated = false;
        //    if (chanceCalculated == false)
        //    {
        //        // Generate a random number between 1 and 25. if the generated number is 3 or less, alert the Wendigo and give it the players position for ~1 second.
        //        alertChance = Random.Range(1, 25);
        //        Debug.Log(alertChance);
        //        if (alertChance <= 3)
        //        {
        //            findingPlayer = true;
        //            AlertWendigo(wendigo);
        //            Debug.Log("ALERTED!!");
        //            alertChance = 9;
        //        }
        //        else if (alertChance > 3)
        //        {
        //            chanceCalculated = false;
        //            findingPlayer = false;
        //            alertChance = 9;
        //        }
        //        chanceCalculated = true;
        //    }
        //
        //}
        //
        //
        //if (findingPlayer == true)
        //{
        //    if (nma.remainingDistance <= 0.8f)
        //    {
        //        findingPlayer = false;
        //        alertChance = 9;
        //        chanceCalculated = false;
        //    }
        //}
        //else if (findingPlayer == false)
        //{
        //    if (moving == false)
        //    {
        //        directionChoice = Random.Range(0, 7);
        //        moveAmount = Random.Range(20, 25);
        //        moving = true;
        //        oldX = wendigo.transform.localPosition.x;
        //        oldZ = wendigo.transform.localPosition.z;
        //        nma.ResetPath();
        //        destinationSet = false;
        //    }
        //
        //    if (moving == true)
        //    {
        //        if (nma.hasPath == false)
        //        {
        //            switch (directionChoice)
        //        {
        //            // Forward
        //            case 0:
        //                {
        //                    if (destinationSet == false)
        //                    {
        //                        DestinationX = (oldX + moveAmount);
        //                        DestinationZ = oldZ;
        //                        destinationSet = true;
        //                    }
        //                    MoveWendigo(wendigo, DestinationX, DestinationZ);
        //                    break;
        //                }
        //            // Backwards
        //            case 1:
        //                {
        //                    if (destinationSet == false)
        //                    {
        //                        DestinationX = (oldX - moveAmount);
        //                        DestinationZ = oldZ;
        //                        destinationSet = true;
        //                    }
        //                    MoveWendigo(wendigo, DestinationX, DestinationZ);
        //                    break;
        //                }
        //            // Right
        //            case 2:
        //                {
        //                    if (destinationSet == false)
        //                    {
        //                        DestinationX = oldX;
        //                        DestinationZ = (oldZ + moveAmount);
        //                        destinationSet = true;
        //                    }
        //                    MoveWendigo(wendigo, DestinationX, DestinationZ);
        //                    break;
        //                }
        //            // Left
        //            case 3:
        //                {
        //                    if (destinationSet == false)
        //                    {
        //                        DestinationX = oldX;
        //                        DestinationZ = (oldZ - moveAmount);
        //                        destinationSet = true;
        //                    }
        //                    MoveWendigo(wendigo, DestinationX, DestinationZ);
        //                    break;
        //                }
        //            // Forward + Right
        //            case 4:
        //                {
        //                    if (destinationSet == false)
        //                    {
        //                        DestinationX = (oldX + moveAmount);
        //                        DestinationZ = (oldZ + moveAmount);
        //                        destinationSet = true;
        //                    }
        //                    MoveWendigo(wendigo, DestinationX, DestinationZ);
        //                    break;
        //                }
        //            // Forward + Left
        //            case 5:
        //                {
        //                    if (destinationSet == false)
        //                    {
        //                        DestinationX = (oldX + moveAmount);
        //                        DestinationZ = (oldZ - moveAmount);
        //                        destinationSet = true;
        //                    }
        //                    MoveWendigo(wendigo, DestinationX, DestinationZ);
        //                    break;
        //                }
        //            // Backwards + Right
        //            case 6:
        //                {
        //                    if (destinationSet == false)
        //                    {
        //                        DestinationX = (oldX - moveAmount);
        //                        DestinationZ = (oldZ + moveAmount);
        //                        destinationSet = true;
        //                    }
        //                    MoveWendigo(wendigo, DestinationX, DestinationZ);
        //                    break;
        //                }
        //            // Backwards + Left
        //            case 7:
        //                {
        //                    if (destinationSet == false)
        //                    {
        //                        DestinationX = (oldX - moveAmount);
        //                        DestinationZ = (oldZ - moveAmount);
        //                        destinationSet = true;
        //                    }
        //                    MoveWendigo(wendigo, DestinationX, DestinationZ);
        //                    break;
        //                }
        //        }
        //        }
        //        if (nma.remainingDistance <= 1)
        //        {
        //            Debug.Log("COMPLETED PROCESS!!");
        //            moving = false;
        //        }
        //    }
        //}
    }

    public void MoveWendigo(WendigoStateManager wendigo, float x, float z)
    {
        if (findingPlayer == false)
        {
            destinationPosition = new Vector3(x, wendigo.transform.localPosition.y, z);
            nma.SetDestination(destinationPosition);
        }
    }

    public void AlertWendigo(WendigoStateManager wendigo)
    {
        nma.isStopped = true;
        nma.ResetPath();
        destinationPosition = new Vector3(wendigo.playerRef.transform.position.x, wendigo.playerRef.transform.position.y, wendigo.playerRef.transform.position.z);
        nma.SetDestination(destinationPosition);
    }
}
