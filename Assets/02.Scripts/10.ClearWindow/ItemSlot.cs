using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("컴포넌트 UI")]
    [SerializeField] private Button btn;
    [SerializeField] private Image highlight;
    [SerializeField] private Image icon;

    // 아이템 슬롯에 넣을 Item 정보
    [Header("아이템 정보")]
    public ISellable item;

    private int index;

    private bool isPurchase = false;
    private ISlotable manager;

    private void Reset()
    {
        btn = transform.FindChild<Button>("ItemSlot");
        highlight = transform.FindChild<Image>("Highlight");
        icon = transform.FindChild<Image>("ItemIcon");
    }

    public void SetManager(ISlotable manager)
    {
        this.manager = manager;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void SetButton(Action<ItemSlot> onClickEvent)
    {
        // Button OnClick 등록
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => onClickEvent(this));
    }

    public void SetItem(ISellable item)
    {
        this.item = item;
        this.icon.sprite = item.GetSpriteIcon();
        this.isPurchase = false;
    }

    public void Click()
    {
        highlight.gameObject.SetActive(true);
    }

    public void UnClick()
    {
        highlight.gameObject.SetActive(false);
    }

    public void ResetSlot()
    {
        highlight.gameObject.SetActive(false);
        this.icon.sprite = null;
        this.item = null;
    }

    public void Purchase()
    {
        isPurchase = true;
        ResetSlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && manager.GetSlotTpye() != SlotType.Shop)
        {
            // DragSlot 설정
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.SetDragItem(this.icon.sprite);
            DragSlot.instance.transform.position = eventData.position;

            UnClick();

            this.icon.color = new Color(1, 1, 1, 0);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && manager.GetSlotTpye() != SlotType.Shop)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (manager.GetSlotTpye() == SlotType.Shop) return;

        DragSlot.instance.dragSlot.icon.color = new Color(1, 1, 1, 1);
        this.icon.color = new Color(1, 1, 1, 1);

        DragSlot.instance.HideDragItem();
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (manager.GetSlotTpye() == SlotType.Shop) return;

        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        ItemSlot sourceSlot = DragSlot.instance.dragSlot;
        ISellable sourceItem = sourceSlot.item;

        ISellable targetItem = this.item;

        this.SetItem(sourceItem);

        manager.OnClickSlot(this);

        if (targetItem != null)
        {
            sourceSlot.SetItem(targetItem);
        }
        else
        {
            sourceSlot.ResetSlot();
        }
    }

    #region 프로퍼티 

    public ISellable Item { get { return item; } }
    public bool IsPurchase { get { return isPurchase; } }

    #endregion
}
