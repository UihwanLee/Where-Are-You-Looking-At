using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdviceSlot : ItemSlot
{
    [Header("UI 컴포넌트")]
    [SerializeField] private Image lockBG;

    public override void Reset()
    {
        btn = transform.GetComponent<Button>();
        highlight = transform.FindChild<Image>("HighlightBG");
        icon = transform.FindChild<Image>("Icon");
        lockBG = transform.FindChild<Image>("LockBG");
    }

    public void OnLock()
    {
        isLock = true;

        highlight.gameObject.SetActive(false);
        lockBG.gameObject.SetActive(true);
    }

    public void UnLock()
    {
        isLock = false;

        highlight.gameObject.SetActive(false);
        lockBG.gameObject.SetActive(false);
    }

    public override void ResetSlot()
    {
        AdvisorSlotManager advisorSlotManager = this.manager as AdvisorSlotManager;

        if(advisorSlotManager != null)
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

    public override void OnDrop(PointerEventData eventData)
    {
        ISellable item = DragSlot.instance.dragSlot.item;
        if (item != null)
        {
            // Advice 타입인지 확인
            if (item.GetType() != SellItemType.Advice) return;

            // 현재 UnLock되어 있는 상태인지 확인
            if (isLock) return;
        }

        base.OnDrop(eventData);

        if(DragSlot.instance.dragSlot.Manager.GetSlotTpye() == Manager.GetSlotTpye())
        {
            // 같은 슬롯 영역에서 옮기면 return
            ClearWindowManager.Instance.SetPlayerStatUI();
            return;
        }

        if(item != null)
        {
            Player player = GameManager.Instance.Player;

            Advice advice = item as Advice;

            if (advice != null)
            {
                player.AdviceHandler.ApplyAdvice(advice);
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if (DragSlot.instance.dragSlot != this)
            {
                if (DragSlot.instance.dragSlot.item.GetType() == SellItemType.Advice)
                    Click();
            }
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if (DragSlot.instance.dragSlot.item.GetType() == SellItemType.Advice)
                UnClick();
        }
    }
}
