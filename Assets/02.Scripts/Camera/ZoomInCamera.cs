using System.Collections;
using UnityEngine;
using Cinemachine;

public class ZoomInCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private float startCam = 30f;
    private float endCam = 6f;
    private float coolTime = 2f;

    private Transform camTr;
    private float startY = 25f;
    private float endY = 0f;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camTr = GetComponent<Transform>();
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

            float newY = Mathf.Lerp(startY, endY, t);
            camTr.localPosition = new Vector3(camTr.localPosition.x, newY, camTr.localPosition.z);

            yield return null;
        }
        cam.m_Lens.OrthographicSize = endCam;
        camTr.localPosition = new Vector3(camTr.localPosition.x, endY, camTr.localPosition.z);
    }
}