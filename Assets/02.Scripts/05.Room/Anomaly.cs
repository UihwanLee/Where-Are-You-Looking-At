using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Anomaly : MonoBehaviour
{
    public Light2D playerLight;
    public Light2D globalLight;
    public Sprite baseSprite;
    public Sprite anomalySprite;
    public GameObject[] objects;

    public bool isAnomaly = false;





    private void Start()
    {
        playerLight = GameObject.Find("Light").GetComponent<Light2D>();
        globalLight = GameObject.Find("Global Light 2D").GetComponent<Light2D>();
        ResetAnomaly();// 처음시작시 셋팅
    }

    public void initAnomaly() //초기화 및 재실행
    {
        ResetAnomaly();
        setAnomaly();
    }

    private void setAnomaly()
    {
        isAnomaly = Random.value < 0.5f; // 50% 확률로 불값 변경
        if (!isAnomaly)
        {
            ResetAnomaly();
            return;
        }

        int random = Random.Range(0, 4); // 간단 가중치 = 0~3 중 하나 뽑기 아래는 그 케이스
        switch (random)
        {
            case 0:
                playerLight.pointLightOuterRadius = 50;
                break;

            case 1:
                globalLight.intensity = 1f;
                break;

            case 2:
                globalLight.intensity = 0f;
                playerLight.pointLightOuterRadius = 3f;
                break;

            case 3:
                objects[Random.Range(0, objects.Length)].GetComponent<SpriteRenderer>().sprite = anomalySprite;//배치된 오브젝트 중 하나의 오브젝트 스프라이트를 이상현상스프라이트로 변경
                break;
        }

    }
    private void ResetAnomaly()//초기화 처음 상태 유지
    {
        playerLight.pointLightOuterRadius = 8f;
        globalLight.intensity = 0.1f;
        foreach (var obj in objects)
        {
            obj.GetComponent<SpriteRenderer>().sprite = baseSprite;
        }
    }
}
