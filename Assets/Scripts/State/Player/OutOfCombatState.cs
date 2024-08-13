using System.Collections;
using UnityEngine;

public class OutOfCombatState : State
{
    public override void Enter()
    {
        Debug.Log("Enter Out Of Combat");
    }

    public override IEnumerator Exit()
    {        
        Debug.Log("Exit Out Of Combat");
        yield return null;
    }

    public override void Update()
    {
    }
}
