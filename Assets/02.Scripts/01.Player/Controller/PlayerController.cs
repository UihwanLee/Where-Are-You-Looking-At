using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    [SerializeField] private AnimationHandler animaton;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveDirection = context.ReadValue<Vector2>();
            moveSpeed = GameManager.Instance.Player.Stat.Speed;
        }
        else if (context.phase == InputActionPhase.Canceled)
            moveDirection = Vector2.zero;

        animaton.Move(moveDirection);
    }

    public override void Death()
    {
        base.Death();
        GameManager.Instance.StopPlayMode();
    }
}
