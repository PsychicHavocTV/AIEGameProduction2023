using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoAnimEvent : MonoBehaviour
{
    public WendigoStateManager wendigoStateManager;

    public void DoGameOver()
    {
        GameManager.Instance.DoGameOver(wendigoStateManager.Wendigo, wendigoStateManager.playerRef);
    }
}
