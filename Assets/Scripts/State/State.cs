using System;
using System.Collections;
using UnityEngine;

public abstract class State
{
    protected  MonoBehaviour Owner;
    public event Action OnExitState;

    protected void InvokeExitState()
    {
        OnExitState?.Invoke();
    }
    
    public abstract void Enter();

    public virtual IEnumerator Exit(bool isForce = false, float duration = 0)
    {
        if (isForce)
        {
            Owner.StopAllCoroutines();
            InvokeExitState();
            yield break;
        }

        yield return new WaitForSeconds(duration);
        InvokeExitState();
    }
    public abstract void Update();
}