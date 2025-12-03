using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AdvisorSlot : MonoBehaviour
{
    [Header("UI 컴포넌트")]
    [SerializeField] private Image highlightBG;
    [SerializeField] private Image icon;
    [SerializeField] private Image lockBG;

    [Header("Advice 슬롯")]
    [SerializeField] List<AdviceSlot> adviceSlots = new List<AdviceSlot>();

    private bool isLock;

    private void Awake()
    { 
        adviceSlots = transform.GetComponentsInChildren<AdviceSlot>().ToList();
    }

    public void Reset()
    {
        highlightBG = transform.FindChild<Image>("HighlightBG");
        icon = transform.FindChild<Image>("Icon");
        lockBG = transform.FindChild<Image>("LockBG");

        adviceSlots = transform.GetComponentsInChildren<AdviceSlot>().ToList();
        foreach (AdviceSlot slot in adviceSlots)
        {
            slot.Reset();
        }
    }

    public void OnLock(Sprite icon_lock)
    {
        isLock = true;
        highlightBG.gameObject.SetActive(false);
        lockBG.gameObject.SetActive(true);
        icon.sprite = icon_lock;
        foreach(AdviceSlot slot in adviceSlots)
        {
            slot.OnLock();
        }
    }

    public void UnLock(Sprite icon_unLock)
    {
        isLock = false;
        highlightBG.gameObject.SetActive(false);
        lockBG.gameObject.SetActive(false);
        icon.sprite = icon_unLock;
        foreach (AdviceSlot slot in adviceSlots)
        {
            slot.UnLock();
        }
    }

    #region 프로퍼티

    public bool IsLock {  get { return isLock; } }

    #endregion
}
