using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();      // 재활용 오브젝트에 담을 Queue
    private GameObject prefab;                                     // 복사하여 사용할 원본 오브젝트
    private Transform parent;                                      // 재활용할 오브젝트를 모아둘 부모 프로젝트

    /// <summary>
    /// ObjectPool 생성
    /// </summary>
    /// <param name="prefab">Pool에 생성할 오브젝트</param>
    /// <param name="initialSize">생성 개수</param>
    /// <param name="parent">생성 Transform</param>
    public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// Pool에서 오브젝트 가져오기
    /// </summary>
    /// <returns>Pool 오브젝트</returns>
    public GameObject Get()
    {
        // Pool에서 해당 오브젝트가 없을 시 생성해서 반환
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : GameObject.Instantiate(prefab, parent);

        // 오브젝트 활성화
        obj.gameObject.SetActive(true);

        // 오브젝트 반환
        return obj;
    }

    /// <summary>
    /// Pool에 오브젝트 반환
    /// </summary>
    public void Release(GameObject obj)
    {
        // 다 사용한 오브젝트는 비활성화하고 Pool에 반납
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
