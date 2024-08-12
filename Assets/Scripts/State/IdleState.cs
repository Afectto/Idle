using System.Collections;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        Debug.Log("Character is Idle.");
    }
    
    public override IEnumerator Exit()
    {
        yield return null;
    }
    
    public override void Update() { /* Логика для состояния ожидания */ }
}