using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdviceSlot : MonoBehaviour
{
    [Header("UI 컴포넌트")]
    [SerializeField] private Image highlightBG;
    [SerializeField] private Image icon;
    [SerializeField] private Image lockBG;

    public void Reset()
    {
        highlightBG = transform.FindChild<Image>("HighlightBG");
        icon = transform.FindChild<Image>("Icon");
        lockBG = transform.FindChild<Image>("LockBG");
    }

    public void OnLock()
    {
        highlightBG.gameObject.SetActive(false);
        lockBG.gameObject.SetActive(true);
    }

    public void UnLock()
    {
        highlightBG.gameObject.SetActive(false);
        lockBG.gameObject.SetActive(false);
    }
}
