using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ClearWindowManager : MonoBehaviour
{
    [Header("Stat 정보")]
    [SerializeField] private PlayerStatUI playerStatUI;
    [SerializeField] private AdvisorStatUI advisorStatUI;
    [SerializeField] private ItemInfoUI itemInfoUI;

    [Header("리스트 정보")]
    [SerializeField] private List<Attribute> playerAttributes = new List<Attribute>();
    [SerializeField] private List<Attribute> advisorAttributes = new List<Attribute>();

    // Stat Change 이벤트
    public Action<List<Attribute>> OnPlayerStatChange;    // 플레이어 Stat Change 이벤트
    public Action<List<Attribute>> OnAdvisorStatChange;   // Advisor Stat Chnage 이벤트

    // OnClick 이벤트

    // ShopManger && InventoryManager -> OnClickSlot 누를 시 ItemInfoUI UpdateUI 수행
    public Action<ISellable> OnClickItemSlotEvent;          // Item Slot Click 이벤트

    // ShopManager -> PurchaseItem을 누를 시 InventoryManager AddItem 수행
    public Action<ISellable> OnClickPurchaseItem;           // Item Purchase Click 이벤트

    public static ClearWindowManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private ClearWindowManager() { }

    private void Reset()
    {
        playerStatUI = transform.FindChild<PlayerStatUI>("PlayerStatWindow");
        advisorStatUI = transform.FindChild<AdvisorStatUI>("AdvisorStatWindow");
        itemInfoUI = transform.FindChild<ItemInfoUI>("ItemInfoWindow");
    }

    private void Start()
    {
        // Player Stat Attribute를 가져와서 LocalIndex로 정렬하여 리스트화
        Player player = GameManager.Instance.Player;

        if (GameManager.Instance.Player == null) Debug.Log("플레이어가 존재 X");
        if (GameManager.Instance.Player.Stat == null) Debug.Log("Stat 컴포넌트 존재 X");
        playerAttributes = GameManager.Instance.Player.Stat.AttributeDict.Values.OrderBy(attribute => attribute.LocalIndex).ToList();

        UpdateStatUI();
        SetPlayerStatUI();
    }

    public void ResetAllInfoUI()
    {
        playerStatUI.SetWindow(false);
        advisorStatUI.SetWindow(false);
        itemInfoUI.SetWindow(false);
    }

    public void SetPlayerStatUI()
    {
        playerStatUI.SetWindow(true);
        advisorStatUI.SetWindow(false);
        itemInfoUI.SetWindow(false);
    }

    private void UpdateStatUI()
    {
        // 모든 StatUI 갱신
        if (OnPlayerStatChange == null) { Debug.Log("등록된 이벤트가 없음!"); return; }
        OnPlayerStatChange.Invoke(playerAttributes);
    }
}
