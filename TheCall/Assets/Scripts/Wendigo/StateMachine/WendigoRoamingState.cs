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
    float playerDistance;
    
    
    
    public override void EnterState(WendigoStateManager wendigo)
    {
        nma.speed = 8;
        nma.ResetPath();
        Debug.Log("Roaming State Entered..");
    }


    public override void UpdateState(WendigoStateManager wendigo)
    {
        playerDistance = Vector3.Distance(wendigo.playerRef.transform.position, wendigo.transform.position);
        //Debug.Log(playerDistance);
        RaycastHit hit;

        if (playerDistance <= 34)
        {
            if (Physics.Raycast(wendigo.RaycastOrigin.transform.position, wendigo.RaycastOrigin.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.tag);
                if (hit.transform.gameObject.tag == "Player")
                {
                    //Debug.Log("Found Player!");
                    //AlertWendigo(wendigo);
                    //findingPlayer = true;
                    wendigo.StartChasing();
                }
            }
        }


        if (wendigo.pController.takingPhoto == true)
        {
            if (playerDistance <= 15)
            {
                wendigo.StartChasing();
            }
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
                moveAmount = Random.Range(15, 25);
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
                    //Debug.Log("COMPLETED PROCESS!!");
                    moving = false;
                }
            }
        }
    }

    public void MoveWendigo(WendigoStateManager wendigo, float x, float z)
    {
        if (findingPlayer == false)
        {
            destinationPosition = new Vector3(x, wendigo.transform.localPosition.y, z);//wendigo.playerRef.transform.position;
            var path = new NavMeshPath();
            nma.CalculatePath(destinationPosition, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                nma.SetPath(path);
                //nma.SetDestination(destinationPosition);
            }
        }
    }

    public void AlertWendigo(WendigoStateManager wendigo)
    {
        nma.ResetPath();
        //var path = new NavMeshPath();
        destinationPosition = wendigo.playerRef.transform.position;
        //if (path.status == NavMeshPathStatus.PathComplete)
        //{
        nma.SetDestination(destinationPosition);
        //    nma.SetPath(path);
        //}
        //nma.SetDestination(destinationPosition);
    }
}
