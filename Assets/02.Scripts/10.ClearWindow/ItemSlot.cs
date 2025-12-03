using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // 상품 슬롯은 8개로 고정
    private int MAX_PRDOUCT_SLOT = 8;

    // 아이템 슬롯에 넣을 Item 정보
    [Header("아이템 정보")]
    [SerializeField] private ISellable item;

    [Header("컴포넌트 UI")]
    [SerializeField] private Button btn;
    [SerializeField] private Image highlight;
    [SerializeField] private Image icon;

    private bool isPurchase = false;

    private void Reset()
    {
        btn = transform.FindChild<Button>("ItemSlot");
        highlight = transform.FindChild<Image>("Highlight");
        icon = transform.FindChild<Image>("ItemIcon");
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

    #region 프로퍼티 

    public ISellable Item { get { return item; } }
    public bool IsPurchase { get { return isPurchase; } }

    #endregion
}
