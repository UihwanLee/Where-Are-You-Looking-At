using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemNameTxt;
    [SerializeField] private Text itemTypeTxt;
    [SerializeField] private Text itemDescTxt;
    [SerializeField] private ClearWindowManager clearWindowManager;

    private void Reset()
    {
        window = transform.GetChild(0).gameObject;
        clearWindowManager = transform.FindParent<ClearWindowManager>("ClearWindow");
        itemIcon = transform.FindChild<Image>("Item_Icon");
        itemNameTxt = transform.FindChild<Text>("Item_Name");
        itemTypeTxt = transform.FindChild<Text>("Item_Type");
        itemDescTxt = transform.FindChild<Text>("Item_Desc");
    }

    private void OnEnable()
    {
        if (clearWindowManager != null)
        {
            Debug.Log("등록: ItemInfo -> OnClickItemSlotEvent 이벤트");
            clearWindowManager.OnClickItemSlotEvent += UpdateInfoUI;
        }
    }

    private void OnDisable()
    {
        if (clearWindowManager != null)
        {
            Debug.Log("해제: ItemInfo -> OnClickItemSlotEvent 이벤트");
            clearWindowManager.OnClickItemSlotEvent -= UpdateInfoUI;
        }
    }

    public void SetWindow(bool active)
    {
        window.SetActive(active);
    }

    public void UpdateInfoUI(ISellable item)
    {

        if (item == null) return;

        clearWindowManager.ResetAllInfoUI();
        SetWindow(true);

        itemIcon.sprite = item.GetSpriteIcon();
        itemNameTxt.text = item.GetName();
        itemTypeTxt.text = item.GetType().ToString();
        itemDescTxt.text = item.GetDescription();
    }
}
