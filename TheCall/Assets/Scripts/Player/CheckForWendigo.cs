using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForWendigo : MonoBehaviour
{
    public WendigoStateManager wsm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wendigo")
        {
            StartCoroutine(wsm.WatchTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wendigo")
        {
            StopCoroutine(wsm.WatchTimer());
        }
    }
}
