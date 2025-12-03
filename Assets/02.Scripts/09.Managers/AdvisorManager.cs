using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvisorManager : MonoBehaviour
{
    const int MaxAdvisorCount = 6;

    [field: SerializeField] private List<GameObject> advisorPrefabs = new List<GameObject>();

    private int currentAdvisorIndex = 0;
    private Vector3[] advisorPivots = new Vector3[MaxAdvisorCount];
    private float radius = 1f;

    private GameObject[] activeAdvisor = new GameObject[MaxAdvisorCount];

    private void Start()
    {
        ArrangementPivots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddAdvisors();
        }
    }

    public void AddAdvisors()
    {
        if (currentAdvisorIndex >= MaxAdvisorCount) return;

        // 1. ������ ����
        GameObject go = Instantiate(advisorPrefabs[0], transform);
        activeAdvisor[currentAdvisorIndex] = go;
        currentAdvisorIndex++;

        ArrangementPivots();

        // 2. ������ ��ġ ���ġ
        for (int i = 0; i < activeAdvisor.Length; i++)
        {
            if (activeAdvisor[i] == null) continue;
            activeAdvisor[i].transform.localPosition = advisorPivots[i];
        }
    }

    private void ArrangementPivots()
    {
        float angleStep = 360f / currentAdvisorIndex;

        for (int i = 0; i < currentAdvisorIndex; i++)
        {
            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius);

            advisorPivots[i] = pos;
        }
    }
}
