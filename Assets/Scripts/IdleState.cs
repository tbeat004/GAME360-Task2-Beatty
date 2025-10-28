using UnityEngine;

public class IdleState : State
{
    private PlayerController player;
    private Renderer playerRenderer;

    public IdleState(StateMachine stateMachine, PlayerController player) : base(stateMachine)
    {
        this.player = player;
        this.playerRenderer = player.GetComponent<Renderer>();
    }

    public override void Enter()
    {
        Debug.Log("Entered Idle State");
        
        // Change color to Blue for Idle
        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.blue;
        }
    }

    public override void Execute()
    {
        // Check for input to transition to other states
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        // Transition to Moving if player presses movement keys
        if (horizontal != 0 || vertical != 0)
        {
            stateMachine.ChangeState(player.movingState);
        }
        // Transition to Jumping if player presses space
        else if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
        {
            stateMachine.ChangeState(player.jumpingState);
        }
        // Transition to Attacking if player shoots
        else if (Input.GetMouseButton(0))
        {
            stateMachine.ChangeState(player.attackingState);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exited Idle State");
    }
}
