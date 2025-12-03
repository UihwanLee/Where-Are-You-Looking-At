using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdvisorType
{
    Projectile
}

public class AdvisorManager : MonoBehaviour
{
    [field: SerializeField] private List<GameObject> advisorPrefabs = new List<GameObject>();

    private Vector3[] advisorPivots = new Vector3[6];

    private float radius = 1f;
    private int currentAdvisorIndex = 0;

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
        if (currentAdvisorIndex >= advisorPivots.Length) return;

        GameObject go = Instantiate(advisorPrefabs[0], transform);
        go.transform.localPosition += advisorPivots[currentAdvisorIndex];
        currentAdvisorIndex++;
    }

    private void ArrangementPivots()
    {
        float angleStep = 360f / advisorPivots.Length;

        for (int i = 0; i < advisorPivots.Length; i++)
        {
            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius);

            advisorPivots[i] = pos;
        }
    }
}
