using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileAdvisorController : BaseAdvisorController
{
    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private float attackTimer = 0f;

    private void Update()
    {
        attackTimer += Time.deltaTime;

        // find target
        FindNearestTarget();

        if (currentTarget != null)
        {
            Rotate();
            if (attackTimer >= stat.AttackSpeed)
            {
                Shoot();
                attackTimer = 0f;
            }
        }
    }

    private void Rotate()
    {
        Vector2 dir = currentTarget.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        spriteRenderer.flipY = Mathf.Abs(angle) > 90;
    }

    private void Shoot()
    {
        GameObject go = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        ProjectileController proj = go.GetComponent<ProjectileController>();
        proj.Init(range:stat.AttackRange, atk:stat.Atk, isFlipY:spriteRenderer.flipY);
    }
}
