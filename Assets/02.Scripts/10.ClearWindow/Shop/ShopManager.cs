using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
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
    [SerializeField] private List<ISellable> productList = new List<ISellable>();

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
    }

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
            slot.Initialize(randomItem, OnClickSlot);
        }
    }

    public void OnClickSlot(ItemSlot slot)
    {
        if(currentSlot != null)
        {
            currentSlot.ResetSlot();
        }

        if(currentSlot == slot)
        {
            // 이미 선택한 슬롯이었다면 초기화
            currentSlot.ResetSlot();
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
}
