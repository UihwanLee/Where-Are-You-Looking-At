using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected Vector2 moveDirection = Vector2.zero;
    public Vector2 MoveDirection { get { return moveDirection; } }

    [Header("Status")]
    protected float moveSpeed = 5f;

    protected virtual void FixedUpdate()
    {
        Movement();
        Flip();
    }

    private void Movement()
    {
        Vector3 direction = transform.right * moveDirection.x + transform.up * moveDirection.y;
        Vector3 moveTransform = direction.normalized * moveSpeed * Time.deltaTime;
        transform.position += moveTransform;
    }

    private void Flip()
    {
        if (moveDirection.x < 0)
            spriteRenderer.flipX = true;
        else if (moveDirection.x > 0)
            spriteRenderer.flipX = false;
    }

    public virtual void Death()
    {
    }
}
