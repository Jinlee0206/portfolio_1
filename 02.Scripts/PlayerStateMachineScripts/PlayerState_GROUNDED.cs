using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_GROUNDED : pState
{
    protected float speed;

    private float horizontalInput;
    private float verticalInput;


    public PlayerState_GROUNDED(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        horizontalInput = verticalInput = 0.0f;
        
    }

    public override void Exit()
    {
        player.ResetMoveParams();
    }

    public override void PhysicsUpdate()
    {

        //player.MoveTo(player.cameraTransform.rotation * new Vector3(horizontalInput * speed, 0, verticalInput * speed));
        //player.transform.rotation = Quaternion.Euler(0, player.cameraTransform.eulerAngles.y, 0);

    }

    public override void LogicUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        player.Move(horizontalInput * speed, verticalInput * speed);

    }
}
