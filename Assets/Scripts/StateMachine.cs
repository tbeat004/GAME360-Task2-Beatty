using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;
    
    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
    
    private void Update()
    {
        currentState?.Execute();
    }
}
