using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public Image itemImage;
    public ItemSlot dragSlot;

    void Awake()
    {
        instance = this;
        HideDragItem();
    }

    public void SetDragItem(Sprite sprite)
    {
        // 아이템 이미지 설정
        itemImage.sprite = sprite;
        itemImage.color = new Color(1, 1, 1, 1);
    }

    public void HideDragItem()
    {
        // 드래그 종료 시 숨기기
        itemImage.color = new Color(1, 1, 1, 0);
    }
}
