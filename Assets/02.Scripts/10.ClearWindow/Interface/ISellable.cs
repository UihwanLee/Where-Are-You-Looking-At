using UnityEngine;

public enum SellItemType
{
    Advice,
    Advisor,
}

public interface ISellable
{
    string GetName();
    string GetDescription();
    SellItemType GetType();
    Sprite GetSpriteIcon();
}
