using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WendigoRoamingState : BaseState
{
    int directionChoice = 0;
    public int moveAmount = 0;
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
        Debug.Log("Roaming..");
        GameManager.Instance.wendigoRoaming = true;
        nma.speed = wendigo.roamSpeed;
        nma.acceleration = nma.speed;
        wendigo.stateAnimator.ResetTrigger(wendigo.chaseParam);
        wendigo.stateAnimator.SetTrigger(wendigo.walkingParam);
        nma.ResetPath();
    }

    public override void UpdateState(WendigoStateManager wendigo)
    {
        if (GameManager.Instance.GamePaused == true)
        {
            nma.acceleration = 0;
            nma.speed = 0;
            nma.enabled = false;
        }
        else if (GameManager.Instance.GameOver == false)
        {
            if (nma.enabled == false)
            {
                nma.enabled = true;
                directionChoice = Random.Range(0, 7);
                moveAmount = Random.Range(15, 55);
                oldX = wendigo.transform.localPosition.x;
                oldZ = wendigo.transform.localPosition.z;
                nma.ResetPath();
                destinationSet = false;
                moving = true;
            }
            if (nma.acceleration == 0)
            {
                nma.acceleration = wendigo.roamSpeed;
            }
            if (nma.speed == 0)
            {
                nma.speed = wendigo.roamSpeed;
            }
            playerDistance = Vector3.Distance(wendigo.playerRef.transform.position, wendigo.transform.position);
            if (playerDistance >= 175)
            {
                nma.speed = 20;
            }
            else
            {
                nma.speed = wendigo.roamSpeed;
            }

            RaycastHit hit;

            if (playerDistance <= 70)
            {
                Vector3 rayDirection = wendigo.playerRef.transform.position - wendigo.transform.position;
                if ((Vector3.Angle(rayDirection, wendigo.transform.forward)) < 65) //Physics.Raycast(wendigo.RaycastOrigin.transform.position, wendigo.RaycastOrigin.transform.TransformDirection(Vector3.forward), out hit, 70, wendigo.layerMask))
                {
                    if ((Vector3.Angle(rayDirection, wendigo.transform.forward)) < 65) // Is player within field of view
                    {
                        if (Physics.Raycast(wendigo.transform.position, rayDirection, out hit, 130))
                        {
                            if (hit.collider.gameObject.tag == "Player")
                            {
                                //Debug.Log("player in front");
                                wendigo.StartChasing();
                            }
                        }
                    }
                }
            }

            if (moving == false)
            {
                directionChoice = Random.Range(0, 7);
                moveAmount = Random.Range(15, 55);
                oldX = wendigo.transform.localPosition.x;
                oldZ = wendigo.transform.localPosition.z;
                nma.ResetPath();
                destinationSet = false;
                moving = true;
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

    // Function for appearing & waiting behind the player.
    public void BehindPlayer()
    {
        nma.isStopped = true;
        nma.ResetPath();
    }

    public void MoveWendigo(WendigoStateManager wendigo, float x, float z)
    {
        if (GameManager.Instance.GameOver == false)
        {
            if (findingPlayer == false)
            {
                destinationPosition = new Vector3(x, wendigo.transform.localPosition.y, z);//wendigo.playerRef.transform.position;
                var path = new NavMeshPath();
                nma.CalculatePath(destinationPosition, path);
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    nma.SetPath(path);
                    nma.SetDestination(destinationPosition);    
                }
            }
        }
    }

    public void AlertWendigo(WendigoStateManager wendigo)
    {
        if (GameManager.Instance.GameOver == false)
        {
            nma.ResetPath();
            var path = new NavMeshPath();
            destinationPosition = wendigo.playerRef.transform.position;
            nma.CalculatePath(destinationPosition, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                nma.SetPath(path);
                nma.SetDestination(destinationPosition);
            }
        }
    }
}
