using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvisorSlotManager : MonoBehaviour
{
    [Header("Test용")]
    [SerializeField] private Sprite icon_unLock;
    [SerializeField] private Sprite icon_lock;

    [Header("조언자 슬롯")]
    [SerializeField] private List<AdvisorSlot> advisorSlots = new List<AdvisorSlot>();

    private int currentUnLockIndex;

    private void Awake()
    {
        advisorSlots = transform.GetComponentsInChildren<AdvisorSlot>().ToList();
        currentUnLockIndex = 0;
        Initialize();
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
        OnLockAll();
        advisorSlots[0].UnLock(icon_unLock);
    }

    private void OnLockAll()
    {
        for(int i=0; i<advisorSlots.Count; i++)
        {
            advisorSlots[i].OnLock(icon_lock);
        }
    }
}
