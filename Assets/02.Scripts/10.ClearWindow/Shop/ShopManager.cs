using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, ISlotable
{
    // 상품 슬롯은 8개로 고정
    private int MAX_PRDOUCT_SLOT = 8;

    // 상품 슬롯 프리팹 부모
    [Header("상품 슬롯 프리팹 부모 Transform")]
    [SerializeField] private Transform productSlotParent;

    // 상품 슬롯 프리팹
    [Header("상품 슬롯 프리팹")]
    [SerializeField] private GameObject productSlotPrefab;

    // 상점에서 활용할 아이템 리스트
    [Header("전체 상품 리스트")]
    [SerializeField] private List<ISellable> itemList = new List<ISellable>();

    // Advice 상품 리스트 -> SO 데이터 개수만큼 Advice 생성
    [Header("Advice 상품 리스트")]
    [SerializeField] private List<AdviceSO> adviceSOs = new List<AdviceSO>();

    // 인터페이스로 상품에 판매할 리스트 묶기
    [Header("상품 리스트")]
    [SerializeField] private List<ItemSlot> productList = new List<ItemSlot>();

    [Header("버튼 컴포넌트")]
    [SerializeField] private Button rerollBtn;
    [SerializeField] private Button purchaseBtn;

    private ItemSlot currentSlot;
    private ClearWindowManager clearWindowManager;

    private void Awake()
    {
        GenerateAdvice();
        GenerateProductSlot();
    }

    private void Start()
    {
        clearWindowManager = ClearWindowManager.Instance;

        SetRerollBtn();
        SetPurchaseBtn();
    }

    private void Reset()
    {
        rerollBtn = transform.FindChild<Button>("Btn_Reroll");
        purchaseBtn = transform.FindChild<Button>("Btn_Purchase");
    }

    private void OnEnable()
    {
        OnClickRerollBtn();
    }

    #region 상품 리스트 설정

    private void GenerateAdvice()
    {
        foreach(var adviceSo in adviceSOs)
        {
            Advice advice = null;

            // Type에 따른 Advice 객체 생성
            switch (adviceSo.TargetType)
            {
                case AdviceTargetType.Player:
                    advice = new PlayerAdvice(adviceSo);
                    break;
                case AdviceTargetType.Advisor:
                    break;
                default:
                    break;
            }

            if (advice != null) itemList.Add(advice);
        }
    }

    private void GenerateProductSlot()
    {
        if (itemList.Count <= 0) return;

        for (int i = 0; i < MAX_PRDOUCT_SLOT; i++)
        {
            int index = i;

            // ProductSlot 생성
            ItemSlot slot = GameObject.Instantiate(productSlotPrefab, productSlotParent.transform).GetComponent<ItemSlot>();

            // ItemList에서 랜덤으로 아이템 정보를 가져와 ProductSlot 초기화하기
            ISellable randomItem = itemList[Random.Range(0, itemList.Count)];

            // ProductSlot 초기화
            slot.SetItem(randomItem);
            slot.SetButton(OnClickSlot);
            slot.UnClick();
            slot.SetManager(this);

            // ProductList에 추가
            productList.Add(slot);
        }
    }

    #endregion

    #region 상품 클릭

    public void OnClickSlot(ItemSlot slot)
    {
        // 이미 구매한 상품이면 return
        if (slot.IsPurchase) return;

        if(currentSlot != null)
        {
            currentSlot.UnClick();
        }

        if(currentSlot == slot)
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
            clearWindowManager.OnClickItemSlotEvent?.Invoke(currentSlot.Item);
        }
    }

    public SlotType GetSlotTpye()
    {
        return SlotType.Shop;
    }

    #endregion

    #region 상품 리롤

    private void SetRerollBtn()
    {
        if (rerollBtn == null) return;

        rerollBtn.onClick.RemoveAllListeners();
        rerollBtn.onClick.AddListener(() => OnClickRerollBtn());
    }

    private void OnClickRerollBtn()
    {
        // 현재 선택한 슬롯 정보 초기화
        if(clearWindowManager != null) clearWindowManager.SetPlayerStatUI();
        if (currentSlot != null)
        {
            currentSlot.ResetSlot();
            currentSlot = null;
        }

        foreach(var product in productList)
        {
            ISellable randomItem = itemList[Random.Range(0, itemList.Count)];
            product.SetItem(randomItem);
        }
    }

    #endregion

    #region 상품 구매

    private void SetPurchaseBtn()
    {
        if(purchaseBtn == null) return;

        purchaseBtn.onClick.RemoveAllListeners();
        purchaseBtn.onClick.AddListener(() => TryPurchase());
    }

    private void TryPurchase()
    {
        Purcahse();
    }

    private void Purcahse()
    {
        if(currentSlot == null) return;

        // 인벤토리에 아이템 넣기
        clearWindowManager.OnClickPurchaseItem?.Invoke(currentSlot.Item);

        // money 정보 갱신

        // 슬롯 구매 정보 전달
        currentSlot.Purchase();

        // 슬롯 초기화
        currentSlot = null;
        clearWindowManager.SetPlayerStatUI();
    }

    #endregion
}
