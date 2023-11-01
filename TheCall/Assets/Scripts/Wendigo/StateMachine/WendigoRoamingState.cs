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
    bool destinationSet = false;
    bool findingPlayer = false;
    Vector3 destinationPosition;
    public NavMeshAgent nma;
    
    
    public override void EnterState(WendigoStateManager wendigo)
    {
        Debug.Log("Roaming State Entered..");
    }


    public override void UpdateState(WendigoStateManager wendigo)
    {
        if (wendigo.pController.takingPhoto == true)
        {
            Debug.Log("ALERTED!!");
            int alertChance = 0;
            alertChance = Random.Range(1, 10);

            if (alertChance <= 3)
            {
                findingPlayer = true;
                AlertWendigo(wendigo);
            }
            else
            {
                findingPlayer = false;
            }
        }

        if (findingPlayer == true)
        {
            if (nma.remainingDistance <= 1.2f)
            {
                findingPlayer = false;
            }
        }
        else if (findingPlayer == false)
        {
            if (moving == false)
            {
                directionChoice = Random.Range(0, 7);
                moveAmount = Random.Range(20, 25);
                moving = true;
                oldX = wendigo.transform.localPosition.x;
                oldZ = wendigo.transform.localPosition.z;
                nma.ResetPath();
                destinationSet = false;
            }

            if (moving == true)
            {
                if (nma.hasPath == false)
                {
                    switch (directionChoice)
                {
                    // Forward
                    case 0:
                        {
                            if (destinationSet == false)
                            {
                                DestinationX = (oldX + moveAmount);
                                DestinationZ = oldZ;
                                destinationSet = true;
                            }
                            MoveWendigo(wendigo, DestinationX, DestinationZ);
                            break;
                        }
                    // Backwards
                    case 1:
                        {
                            if (destinationSet == false)
                            {
                                DestinationX = (oldX - moveAmount);
                                DestinationZ = oldZ;
                                destinationSet = true;
                            }
                            MoveWendigo(wendigo, DestinationX, DestinationZ);
                            break;
                        }
                    // Right
                    case 2:
                        {
                            if (destinationSet == false)
                            {
                                DestinationX = oldX;
                                DestinationZ = (oldZ + moveAmount);
                                destinationSet = true;
                            }
                            MoveWendigo(wendigo, DestinationX, DestinationZ);
                            break;
                        }
                    // Left
                    case 3:
                        {
                            if (destinationSet == false)
                            {
                                DestinationX = oldX;
                                DestinationZ = (oldZ - moveAmount);
                                destinationSet = true;
                            }
                            MoveWendigo(wendigo, DestinationX, DestinationZ);
                            break;
                        }
                    // Forward + Right
                    case 4:
                        {
                            if (destinationSet == false)
                            {
                                DestinationX = (oldX + moveAmount);
                                DestinationZ = (oldZ + moveAmount);
                                destinationSet = true;
                            }
                            MoveWendigo(wendigo, DestinationX, DestinationZ);
                            break;
                        }
                    // Forward + Left
                    case 5:
                        {
                            if (destinationSet == false)
                            {
                                DestinationX = (oldX + moveAmount);
                                DestinationZ = (oldZ - moveAmount);
                                destinationSet = true;
                            }
                            MoveWendigo(wendigo, DestinationX, DestinationZ);
                            break;
                        }
                    // Backwards + Right
                    case 6:
                        {
                            if (destinationSet == false)
                            {
                                DestinationX = (oldX - moveAmount);
                                DestinationZ = (oldZ + moveAmount);
                                destinationSet = true;
                            }
                            MoveWendigo(wendigo, DestinationX, DestinationZ);
                            break;
                        }
                    // Backwards + Left
                    case 7:
                        {
                            if (destinationSet == false)
                            {
                                DestinationX = (oldX - moveAmount);
                                DestinationZ = (oldZ - moveAmount);
                                destinationSet = true;
                            }
                            MoveWendigo(wendigo, DestinationX, DestinationZ);
                            break;
                        }
                }
                }
                if (nma.remainingDistance <= 1)
                {
                    Debug.Log("COMPLETED PROCESS!!");
                    moving = false;
                }
            }
        }
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
