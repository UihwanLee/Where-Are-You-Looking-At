using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, ISlotable
{
    // 인벤토리 슬롯은 18개로 고정
    private int MAX_INVENTORY_SLOT = 18;

    // 인벤토리 슬롯 프리팹 부모
    [Header("인벤토리 슬롯 프리팹 부모 Transform")]
    [SerializeField] private Transform inventorySlotParent;

    // 인벤토리 슬롯 프리팹
    [Header("인벤토리 슬롯 프리팹")]
    [SerializeField] private GameObject inventorySlotPrefab;

    [Header("인벤토리 리스트")]
    [SerializeField] private List<ItemSlot> inventoryList = new List<ItemSlot>();

    private Button saleBtn;
    private Button closeBtn;

    private ItemSlot currentSlot;
    private int currentSlotCount;
    private ClearWindowManager clearWindowManager;

    private void Awake()
    {
        GenerateInventorySlot();
        saleBtn = transform.FindChild<Button>("Btn_Sale");
        closeBtn = transform.FindChild<Button>("Btn_Close");

        saleBtn.onClick.AddListener(()=>SaleItem());
        closeBtn.onClick.AddListener(() => Close());
    }

    private void Start()
    {
        clearWindowManager = ClearWindowManager.Instance;
        currentSlotCount = 0;
        currentSlot = null;
    }

    private void OnEnable()
    {
        clearWindowManager = transform.FindParent<ClearWindowManager>("ClearWindow");
        if (clearWindowManager != null)
        {
            Debug.Log("등록: InventoryManager -> OnClickPurchaseItem 이벤트");
            clearWindowManager.OnClickPurchaseItem += AddItem;
        }
    }

    private void OnDisable()
    {
        if (clearWindowManager != null)
        {
            Debug.Log("해제: InventoryManager -> OnClickPurchaseItem 이벤트");
            clearWindowManager.OnClickPurchaseItem -= AddItem;
        }
    }

    #region 인벤토리 리스트 설정

    private void GenerateInventorySlot()
    {
        for (int i = 0; i < MAX_INVENTORY_SLOT; i++)
        {
            int index = i;

            // InventorySlot 생성
            ItemSlot slot = GameObject.Instantiate(inventorySlotPrefab, inventorySlotParent.transform).GetComponent<ItemSlot>();

            // InventorySlot 초기화
            slot.SetButton(OnClickSlot);
            slot.UnClick();
            slot.SetIndex(index);
            slot.SetManager(this);

            // InventoryList에 추가
            inventoryList.Add(slot);
        }
    }

    #endregion

    #region 슬롯 클릭

    public void OnClickSlot(ItemSlot slot)
    {
        // 빈 슬롯이면 return
        if (slot == null)
        {
            Debug.Log("slot이 Null입니다");
            return;
        }

        if (currentSlot != null)
        {
            currentSlot.UnClick();
        }

        if (currentSlot == slot)
        {
            // 이미 선택한 슬롯이었다면 초기화
            currentSlot.UnClick();
            clearWindowManager.SetPlayerStatUI();
            currentSlot = null;
        }
        else
        {
            // 아니면 버튼 클릭
            currentSlot = slot;
            currentSlot.Click();

            // 아이템 정보 UI 갱신
            if (currentSlot.Item != null) clearWindowManager.OnClickItemSlotEvent?.Invoke(currentSlot.Item);
            else clearWindowManager.SetPlayerStatUI();
        }
    }

    public SlotType GetSlotTpye()
    {
        return SlotType.Inventory;
    }

    #endregion

    #region 인벤토리 아이템 추가/삭제

    public void AddItem(ISellable item)
    {
        ItemSlot emptySlot = inventoryList.Find(slot => slot.Item == null);

        if (emptySlot != null)
        {
            // 비어있는 슬롯에 아이템 추가
            emptySlot.SetItem(item);

            currentSlotCount++;
        }
        else
        {
            // 인벤토리가 가득 찬 경우
            Debug.Log("인벤토리가 가득 찼습니다!");
        }
    }

    public void DeleteItem(ItemSlot slot)
    {
        if (slot == null) return;

        // 현재 InventorySlot 중 해당하는 Item을 Index로 찾아 지운다.
        inventoryList[slot.Index].ResetSlot();

        // Slot Index 감소
        currentSlotCount = Mathf.Max(currentSlotCount - 1, 0);

        // Inventory UI 갱신
        UpdateInventorySlot();
    }

    private void UpdateInventorySlot()
    {
        // 인벤토리 슬롯 업데이트 : 인벤토리 UI 갱신

        List<ItemSlot> activeSlot = new List<ItemSlot>();

        // 인벤토리에 아이템이 저장되어 있는 슬롯 찾기
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].Item != null)
            {
                activeSlot.Add(inventoryList[i]);
            }
        }

        // 인벤토리 아이템들 앞으로 땡기기
        for (int i = 0; i < inventoryList.Count; i++)
        {
            int index = i;
            if (index < activeSlot.Count)
            {
                inventoryList[index].SetItem(activeSlot[index].Item);
            }
            else
            {
                inventoryList[index].ResetSlot();
            }
        }
    }

    #endregion

    #region 아이템 판매

    public void SaleItem()
    {
        if (currentSlot == null) return;

        // 골드 흭득

        // 아이템 삭제
        DeleteItem(currentSlot);
    }

    public void Close()
    {
        List<ItemSlot> activeSlot = new List<ItemSlot>();

        // 인벤토리에 아이템이 저장되어 있는 슬롯 찾기
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].Item != null)
            {
                activeSlot.Add(inventoryList[i]);
            }
        }

        foreach(var slot in activeSlot)
        {
            // 모두 판매하면 서 골드 흭득

            // 슬롯 초기화
            slot.ResetSlot();
        }

        currentSlotCount = 0;

        Player player = GameManager.Instance.Player;
        player.transform.position = new Vector3(25f, 1f);
        clearWindowManager.SetClearWindow(false);
        MonsterSpawner.Instance.StartSpawn();
    }

    #endregion
}
