using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected StateMachine StateMachine;
    protected Stats currentStats;

    public virtual Stats GetCurrentStats() => currentStats;
    
    protected virtual void Start()
    {
        StateMachine = new StateMachine(this);
        StartIdle();
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }
    
    protected void StartIdle()
    {
        StateMachine.ChangeState(new IdleState());
    }

}