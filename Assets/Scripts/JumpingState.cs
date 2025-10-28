using UnityEngine;

public class JumpingState : State
{
    private PlayerController player;
    private Renderer playerRenderer;

    public JumpingState(StateMachine stateMachine, PlayerController player) : base(stateMachine)
    {
        this.player = player;
        this.playerRenderer = player.GetComponent<Renderer>();
    }

    public override void Enter()
    {
        Debug.Log("Entered Jumping State");
    
        // Change color to Yellow for Jumping
        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.yellow;
        }
        
        // Execute jump when entering this state
        player.HandleJumping();
    }

    public override void Execute()
    {
        // Allow movement in air
        player.HandleMovement();
        
        // Can shoot while jumping
        if (Input.GetMouseButton(0))
        {
            stateMachine.ChangeState(player.attackingState);
            return; 
        }
        
        
        // Check if landed - transition back to appropriate state
        if (player.IsGrounded())
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
            if (horizontal != 0 || vertical != 0)
            {
                stateMachine.ChangeState(player.movingState);
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("Exited Jumping State");
    }
}
