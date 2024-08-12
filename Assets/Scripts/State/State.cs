using System.Collections;
using UnityEngine;

public abstract class State
{
    protected  MonoBehaviour Owner;

    public abstract void Enter();
    public abstract IEnumerator Exit();
    public abstract void Update();
}