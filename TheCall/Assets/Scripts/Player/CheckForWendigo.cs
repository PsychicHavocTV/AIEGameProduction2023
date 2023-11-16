using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForWendigo : MonoBehaviour
{
    public WendigoStateManager wsm;


    private void Update()
    {
        if (wsm.isBehindPlayer == true && wsm.timerRunning == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(wsm.playerCamera.transform.position, wsm.playerCamera.transform.forward, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.tag == "Wendigo")
                {
                    Debug.Log("Looking at Wendigo.");
                    StartCoroutine(wsm.WatchTimer());
                }
            }
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Wendigo")
    //    {
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Wendigo")
    //    {
    //        StopCoroutine(wsm.WatchTimer());
    //    }
    //}
}
