using UnityEngine;

public interface ISellable
{
    string GetName();
    string GetDescription();
    string GetType();
    Sprite GetSpriteIcon();
}
