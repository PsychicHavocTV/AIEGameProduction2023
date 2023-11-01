using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Vector3 destinationPosition;
    
    
    public override void EnterState(WendigoStateManager wendigo)
    {
        Debug.Log("Roaming State Entered..");
    }


    public override void UpdateState(WendigoStateManager wendigo)
    {
        if (moving == false)
        {
            directionChoice = Random.Range(0, 7);
            moveAmount = Random.Range(10, 25);
            moving = true;
            oldX = wendigo.transform.localPosition.x;
            oldZ = wendigo.transform.localPosition.z;
            destinationSet = false;
        }

        if (moving == true)
        {
            if (moveAmount > 0)
            {
                // TO BE OVERHAULED & USING NAVMESH
                switch (directionChoice)
                {
                    // Forward
                    case 0:
                        {
                            //if (destinationSet == false)
                            //{
                            //    DestinationX = (oldX + moveAmount);
                            //    DestinationZ = oldZ;
                            //    destinationSet = true;
                            //}
                            //if (wendigo.transform.localPosition.x > DestinationX)
                            //{
                            //    wendigo.transform.localPosition = new Vector3(DestinationX, wendigo.transform.localPosition.y, wendigo.transform.localPosition.z);
                            //}
                            //else
                            //{
                            //    wendigo.transform.localPosition += new Vector3(1, 0, 0) * roamSpeed * Time.deltaTime;
                            //}
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
                            if (wendigo.transform.localPosition.x < DestinationX)
                            {
                                wendigo.transform.localPosition = new Vector3(DestinationX, wendigo.transform.localPosition.y, wendigo.transform.localPosition.z);
                            }
                            else
                            {
                                wendigo.transform.localPosition -= new Vector3(1, 0, 0) * roamSpeed * Time.deltaTime;
                            }
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
                            if (wendigo.transform.localPosition.z > DestinationZ)
                            {
                                wendigo.transform.localPosition = new Vector3(wendigo.transform.localPosition.x, wendigo.transform.localPosition.y, DestinationZ);
                            }
                            else
                            {
                                wendigo.transform.localPosition += new Vector3(0, 0, 1) * roamSpeed * Time.deltaTime;
                            }
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
                            if (wendigo.transform.localPosition.z < DestinationZ)
                            {
                                wendigo.transform.localPosition = new Vector3(wendigo.transform.localPosition.x, wendigo.transform.localPosition.y, DestinationZ);
                            }
                            else
                            {
                                wendigo.transform.localPosition -= new Vector3(0, 0, 1) * roamSpeed * Time.deltaTime;
                            }
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
                            if (wendigo.transform.localPosition.x > DestinationX)
                            {
                                wendigo.transform.localPosition = new Vector3(DestinationX, wendigo.transform.localPosition.y, wendigo.transform.localPosition.z);
                            }
                            else
                            {
                                wendigo.transform.localPosition += new Vector3(1, 0, 0) * roamSpeed * Time.deltaTime;
                            }
                            if (wendigo.transform.localPosition.z > DestinationZ)
                            {
                                wendigo.transform.localPosition = new Vector3(wendigo.transform.localPosition.x, wendigo.transform.localPosition.y, DestinationZ);
                            }
                            else
                            {
                                wendigo.transform.localPosition += new Vector3(0, 0, 1) * roamSpeed * Time.deltaTime;
                            }
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
                            if (wendigo.transform.localPosition.x > DestinationX)
                            {
                                wendigo.transform.localPosition = new Vector3(DestinationX, wendigo.transform.localPosition.y, wendigo.transform.localPosition.z);
                            }
                            else
                            {
                                wendigo.transform.localPosition += new Vector3(1, 0, 0) * roamSpeed * Time.deltaTime;
                            }
                            if (wendigo.transform.localPosition.z < DestinationZ)
                            {
                                wendigo.transform.localPosition = new Vector3(wendigo.transform.localPosition.x, wendigo.transform.localPosition.y, DestinationZ);
                            }
                            else
                            {
                                wendigo.transform.localPosition -= new Vector3(0, 0, 1) * roamSpeed * Time.deltaTime;
                            }
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
                            if (wendigo.transform.localPosition.x < DestinationX)
                            {
                                wendigo.transform.localPosition = new Vector3(DestinationX, wendigo.transform.localPosition.y, wendigo.transform.localPosition.z);
                            }
                            else
                            {
                                wendigo.transform.localPosition -= new Vector3(1, 0, 0) * roamSpeed * Time.deltaTime;
                            }
                            if (wendigo.transform.localPosition.z > DestinationZ)
                            {
                                wendigo.transform.localPosition = new Vector3(wendigo.transform.localPosition.x, wendigo.transform.localPosition.y, DestinationZ);
                            }
                            else
                            {
                                wendigo.transform.localPosition += new Vector3(0, 0, 1) * roamSpeed * Time.deltaTime;
                            }
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
                            if (wendigo.transform.localPosition.x < DestinationX)
                            {
                                wendigo.transform.localPosition = new Vector3(DestinationX, wendigo.transform.localPosition.y, wendigo.transform.localPosition.z);
                            }
                            else
                            {
                                wendigo.transform.localPosition -= new Vector3(1, 0, 0) * roamSpeed * Time.deltaTime;
                            }
                            if (wendigo.transform.localPosition.z < DestinationZ)
                            {
                                wendigo.transform.localPosition = new Vector3(wendigo.transform.localPosition.x, wendigo.transform.localPosition.y, DestinationZ);
                            }
                            else
                            {
                                wendigo.transform.localPosition -= new Vector3(0, 0, 1) * roamSpeed * Time.deltaTime;
                            }
                            break;
                        }
                }
                moveAmount -= 1 * roamSpeed * (int)Time.deltaTime;
                destinationPosition = new Vector3(DestinationX, wendigo.transform.localPosition.y, DestinationZ);
                Debug.Log("oldX: " + oldX);
                Debug.Log("oldZ: " + oldZ);
                Debug.Log("DestinationX: " + DestinationX);
                Debug.Log("DestinationZ: " + DestinationZ);
                Debug.Log("CurrentX: " + wendigo.transform.localPosition.x);
                Debug.Log("CurrentZ: " + wendigo.transform.localPosition.z);
            }
            if (wendigo.transform.localPosition == destinationPosition)
            {
                Debug.Log("COMPLETED PROCESS!!");
                moving = false;
            }
        }
    }

    public void AlertWendigo()
    {

    }
}
