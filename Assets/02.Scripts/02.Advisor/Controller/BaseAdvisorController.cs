using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAdvisorController : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] protected float range;
    [SerializeField] private LayerMask targetMask;

    protected Transform currentTarget;

    protected bool FindNearestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, targetMask);

        if (hits.Length == 0)
        {
            currentTarget = null;
            return false;
        }

        float minDistance = Mathf.Infinity;
        Transform nearestTarget = null;

        foreach (Collider2D hit in hits)
        {
            float dist = (hit.transform.position - transform.position).sqrMagnitude;

            if (dist < minDistance)
            {
                minDistance = dist;
                nearestTarget = hit.transform;
            }
        }

        currentTarget = nearestTarget;
        return true;
    }
}
