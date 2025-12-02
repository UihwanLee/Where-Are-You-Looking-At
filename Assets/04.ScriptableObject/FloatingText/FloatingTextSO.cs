using UnityEngine;

public enum TextType
{
    Damage,
    Gold,
    Exp,
    UNDEF,
}

[CreateAssetMenu(fileName = "FloatingText", menuName = "UI/FloatingText")]
public class FloatingTextSO : ScriptableObject
{
    [field: Header("Text 이름")]
    [field: SerializeField] public string Name { get; private set; }
    [field: Header("Text 타입")]
    [field: SerializeField] public TextType Type { get; private set; }
    [field: Header("Text 색상")]
    [field: SerializeField] public Color Color { get; private set; }
    [field: Header("Text 띄우기 지속시간")]
    [field: SerializeField] public float Duration { get; private set; }
    [field: Header("Text 띄우기 거리")]
    [field: SerializeField] public float FloaingDist { get; private set; }
}
