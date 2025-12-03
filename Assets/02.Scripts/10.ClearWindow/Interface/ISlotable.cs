
// 슬롯 타입
public enum SlotType
{
    Inventory,
    Shop,
    Advisor,
    Advice
}

public interface ISlotable
{
    void OnClickSlot(ItemSlot slot);
    SlotType GetSlotTpye();
}
