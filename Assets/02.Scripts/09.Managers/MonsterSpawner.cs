using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 3f;
    //[SerializeField] private float spawnRadius = 8f;
    [SerializeField] private int spawnCountPerInterval = 3;
    [SerializeField] private int maxMonsters = 30;
    [SerializeField] private Rect spawnArea;

    [Header("Monster Prefab")]
    [SerializeField] GameObject monsterPrefab;

    private Transform player;

    private readonly List<GameObject> activeMonsters = new List<GameObject>();

    private Coroutine currentSpawnRoutine;

    private void Reset()
    {
        spawnInterval = 3f;
        //spawnRadius = 8f;
        spawnCountPerInterval = 3;
        maxMonsters = 30;
    }

    private void Start()
    {
        player = GameManager.Instance.Player.transform;

        PoolManager.Instance.CreatePool("Monster", monsterPrefab, maxMonsters);
        StartSpawn();
    }

    public void StartSpawn()
    {
        currentSpawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (activeMonsters.Count < maxMonsters)
            {
                for (int i = 0; i < spawnCountPerInterval; i++)
                {
                    if (activeMonsters.Count >= maxMonsters) break;

                    SpawnMonster();
                }
            }
        }
    }

    private void SpawnMonster()
    {
        if (monsterPrefab == null || spawnArea == null) return;

        //Vector2 randomDir = Random.insideUnitCircle.normalized;
        //Vector2 spawnPos = (Vector2)player.transform.position + randomDir * spawnRadius;

        Vector2 spawnPos = new Vector2(Random.Range(spawnArea.xMin, spawnArea.xMax), Random.Range(spawnArea.yMin, spawnArea.yMax));

        GameObject monster = PoolManager.Instance.GetObject("Monster");

        monster.transform.position = spawnPos;

        MonsterController monsterController = monster.GetComponent<MonsterController>();
        monsterController.Init(player, this);

        activeMonsters.Add(monster);

        monster.SetActive(true);
    }

    public void RemoveActiveMonster(GameObject monster)
    {
        activeMonsters.Remove(monster);
    }

    public void ClearActiveMonster()
    {
        StopCoroutine(currentSpawnRoutine);

        foreach (GameObject monster in activeMonsters)
        {
            PoolManager.Instance.ReleaseObject("Monster", monster);
        }

        activeMonsters.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnArea == null) return;

        Gizmos.color = Color.magenta;

        Vector3 center = new Vector3(spawnArea.x + spawnArea.width / 2, spawnArea.y + spawnArea.height / 2);
        Vector3 size = new Vector3(spawnArea.width, spawnArea.height);

        Gizmos.DrawCube(center, size);
    }
}
