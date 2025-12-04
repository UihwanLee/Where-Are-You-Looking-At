using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvisorSlotManager : MonoBehaviour, ISlotable
{
    [Header("Test용")]
    [SerializeField] private Sprite icon_unLock;
    [SerializeField] private Sprite icon_lock;
    [SerializeField] private Sprite icon_advisor;

    [Header("조언자 슬롯")]
    [SerializeField] private List<AdvisorSlot> advisorSlots = new List<AdvisorSlot>();

    private ItemSlot currentSlot;
    private int currentUnLockIndex;
    private ClearWindowManager clearWindowManager;

    private void Awake()
    {
        advisorSlots = transform.GetComponentsInChildren<AdvisorSlot>().ToList();
        currentUnLockIndex = 0;
        Initialize();
    }

    private void Start()
    {
        clearWindowManager = ClearWindowManager.Instance;
        currentSlot = null;
    }

    private void OnEnable()
    {
        for (int i = 0; i < advisorSlots.Count; i++)
        {
            advisorSlots[i].OffHighlight();
        }
    }

    public void Reset()
    {
        advisorSlots = transform.GetComponentsInChildren<AdvisorSlot>().ToList();
        for(int i=0; i<advisorSlots.Count; i++)
        {
            advisorSlots[i].Reset();
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < advisorSlots.Count; i++)
        {
            advisorSlots[i].OnLock(icon_lock);
            advisorSlots[i].SetManager(this);
            advisorSlots[i].SetButton(OnClickSlot);
        }
        advisorSlots[0].UnLock(icon_unLock);
        advisorSlots[1].UnLock(icon_unLock);
        advisorSlots[0].SetIcon(icon_advisor);
        advisorSlots[1].SetIcon(icon_advisor);
    }


    #region 슬롯 클릭

    public void OnClickSlot(ItemSlot slot)
    {
        // 빈 슬롯이면 return
        if (slot == null)
        {
            Debug.Log("slot이 Null입니다");
            return;
        }

        // 잠금 되어 있는 슬롯인지 확인
        if (slot.IsLock) return;

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
            if (currentSlot.Item != null)
            {
                // 아이템 정보가 Advice일 시
                if (currentSlot.Item.GetType() == SellItemType.Advice)
                    clearWindowManager.OnClickItemSlotEvent?.Invoke(currentSlot.Item);
                // 아이템 정보가 Advisor일 시
                else if (currentSlot.Item.GetType() == SellItemType.Advisor)
                    Debug.Log("Advisor");
            }
            else clearWindowManager.SetPlayerStatUI();
        }
    }

    public SlotType GetSlotTpye()
    {
        return SlotType.Advisor;
    }

    #endregion

    #region 프로퍼티

    public Sprite Icon_UnLock { get { return icon_unLock; } }
    public Sprite Icon_Lock { get {return icon_lock; } }

    #endregion
}
