using System.Collections;
using UnityEngine;
using Cinemachine;

public class ZoomInCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private CinemachineFramingTransposer transposer;
    private float startCam = 30f;
    private float endCam = 6f;
    private float coolTime = 2f;


    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        transposer = cam.AddCinemachineComponent<CinemachineFramingTransposer>();
        StartCoroutine(ZoomIn());
        

    }

    IEnumerator ZoomIn()
    {
        
        
        float timer = 0f;
        while (timer < coolTime)
        {
            timer += Time.deltaTime;
            float t = timer / coolTime;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(startCam, endCam, t);

            transposer.m_ScreenY = Mathf.Lerp(0.9f, 0.5f, t);


            yield return null;
        }
        cam.m_Lens.OrthographicSize = endCam;
    }
}