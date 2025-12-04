using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdvisorSlot : ItemSlot
{
    [Header("UI 컴포넌트")]
    [SerializeField] private Image lockBG;

    [Header("Advice 슬롯")]
    [SerializeField] List<AdviceSlot> adviceSlots = new List<AdviceSlot>();

    private void Awake()
    { 
        adviceSlots = transform.GetComponentsInChildren<AdviceSlot>().ToList();
    }

    public override void Reset()
    {
        btn = transform.GetComponent<Button>();
        highlight = transform.FindChild<Image>("HighlightBG");
        icon = transform.FindChild<Image>("Icon");
        lockBG = transform.FindChild<Image>("LockBG");

        adviceSlots = transform.GetComponentsInChildren<AdviceSlot>().ToList();
        foreach (AdviceSlot slot in adviceSlots)
        {
            slot.Reset();
        }
    }

    public void OffHighlight()
    {
        highlight.gameObject.SetActive(false);

        foreach (AdviceSlot slot in adviceSlots)
        {
            slot.OffHighlight();
        }
    }

    public override void SetManager(ISlotable manager)
    {
        base.SetManager(manager);

        foreach (AdviceSlot slot in adviceSlots)
        {
            slot.SetManager(manager);
        }
    }

    public override void SetButton(Action<ItemSlot> onClickEvent)
    {
        base.SetButton(onClickEvent);

        foreach (AdviceSlot slot in adviceSlots)
        {
            slot.SetButton(onClickEvent);
        }
    }

    public override void ResetSlot()
    {
        AdvisorSlotManager advisorSlotManager = this.manager as AdvisorSlotManager;

        if (advisorSlotManager != null)
        {
            highlight.gameObject.SetActive(false);
            this.icon.sprite = advisorSlotManager.Icon_UnLock;
            this.item = null;
        }
        else
        {
            base.ResetSlot();
        }
    }

    public void OnLock(Sprite icon_lock)
    {
        isLock = true;
        highlight.gameObject.SetActive(false);
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
        highlight.gameObject.SetActive(false);
        lockBG.gameObject.SetActive(false);
        icon.sprite = icon_unLock;
        foreach (AdviceSlot slot in adviceSlots)
        {
            slot.UnLock();
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot == null) return;

        ISellable item = DragSlot.instance.dragSlot.item;
        if (item != null)
        {
            // Advice 타입인지 확인
            if (item.GetType() != SellItemType.Advisor) return;

            // 현재 UnLock되어 있는 상태인지 확인
            if (isLock) return;
        }

        base.OnDrop(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if (DragSlot.instance.dragSlot != this)
            {
                if(DragSlot.instance.dragSlot.item.GetType() == SellItemType.Advisor)
                    Click();
            }
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if (DragSlot.instance.dragSlot.item.GetType() == SellItemType.Advisor)
                UnClick();
        }
    }
}
