using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ProjectileAdvisorController : BaseAdvisorController
{
    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Transform firePoint;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private float rotateSpeed = 1f;
    private float fireTimer = 0f;

    private void Update()
    {
        fireTimer += Time.deltaTime;

        // �Ź� ���� ����� Ÿ�� Ž��
        FindNearestTarget();

        if (currentTarget != null)
        {
            Rotate();
            if (fireTimer >= attackSpeed)
            {
                Shoot();
                fireTimer = 0f;
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
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
