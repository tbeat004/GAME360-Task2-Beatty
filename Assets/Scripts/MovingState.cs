using UnityEngine;

public class MovingState : State
{
    private PlayerController player;
    private Renderer playerRenderer;

    public MovingState(StateMachine stateMachine, PlayerController player) : base(stateMachine)
    {
        this.player = player;
        this.playerRenderer = player.GetComponent<Renderer>();
    }

    public override void Enter()
    {
        Debug.Log("Entered Moving State");
        
        // Change color to Green for Moving
        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.green;
        }
    }

    public override void Execute()
    {
        // Handle movement
        player.HandleMovement();
        
        // Check for input to transition to other states
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        // Transition to Idle if not moving
        if (horizontal == 0 && vertical == 0)
        {
            stateMachine.ChangeState(player.idleState);
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
        Debug.Log("Exited Moving State");
    }
}
