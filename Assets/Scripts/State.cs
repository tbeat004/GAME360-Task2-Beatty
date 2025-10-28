using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    // Called when entering the state
    public abstract void Enter();

    // Called every frame while in this state
    public abstract void Execute();

    // Called when exiting the state
    public abstract void Exit();
}
