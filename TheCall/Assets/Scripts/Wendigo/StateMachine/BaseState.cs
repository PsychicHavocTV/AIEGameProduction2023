using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(WendigoStateManager wendigo);

    public abstract void UpdateState(WendigoStateManager wendigo);
}
