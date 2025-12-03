using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private float lifeDistance;
    private Vector3 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        float moveDistance = (startPosition - transform.position).magnitude;
        if (moveDistance > lifeDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Monster>(out var monster))
        {
            monster.Condition.TakeDamage(transform, damage);
            Destroy(gameObject);
        }
    }

    public void Init(float range, float atk, bool isFlipY)
    {
        this.lifeDistance = range;
        this.damage = atk;
        spriteRenderer.flipY = isFlipY;
    }
}
