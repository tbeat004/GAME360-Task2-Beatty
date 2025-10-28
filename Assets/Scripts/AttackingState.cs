using UnityEngine;

public class AttackingState : State
{
    private PlayerController player;
    private Renderer playerRenderer;

    public AttackingState(StateMachine stateMachine, PlayerController player) : base(stateMachine)
    {
        this.player = player;
        this.playerRenderer = player.GetComponent<Renderer>();
    }

    public override void Enter()
    {
        Debug.Log("Entered Attacking State");
        
        // Change color to Red for Attacking
        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.red;
        }
    }

    public override void Execute()
    {
        // Handle shooting
        player.HandleShooting();
        
        // Can still move while shooting
        player.HandleMovement();
        
        // Can jump while shooting
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
        {
            stateMachine.ChangeState(player.jumpingState);
        }
        
        // Return to appropriate state when not shooting
        if (!Input.GetMouseButton(0))
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
            if (!player.IsGrounded())
            {
                stateMachine.ChangeState(player.jumpingState);
            }
            else if (horizontal != 0 || vertical != 0)
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
        Debug.Log("Exited Attacking State");
    }
}
