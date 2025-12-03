using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int InMoving = Animator.StringToHash("IsMove");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 moveDirection)
    {
        animator.SetBool(InMoving, moveDirection.magnitude > 0.1f);
    }
}
