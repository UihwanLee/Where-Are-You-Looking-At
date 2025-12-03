using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    private MonsterStat stat;

    private Transform target;
    MonsterSpawner spawner;

    private void Awake()
    {
        stat = GetComponent<MonsterStat>();
    }

    private void Start()
    {
        moveSpeed = stat.Speed;

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    protected override void FixedUpdate()
    {
        if (target == null) return;

        moveDirection = target.position - transform.position;

        if (moveDirection.magnitude < 0.1f) return;

        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // player trigger effect
    }

    public void Init(Transform playerTransform, MonsterSpawner spawner)
    {
        target = playerTransform;
        this.spawner = spawner;
    }

    private void Die()
    {
        spawner.RemoveActiveMonster(gameObject);
        PoolManager.Instance.ReleaseObject("Monster", gameObject);
    }
}
