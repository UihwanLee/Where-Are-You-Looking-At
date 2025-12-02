using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTargetType
{
    Player,
    Advisor,
}

[System.Serializable]
public class EffectInfo
{
    [field: Header("이름")]
    [field: SerializeField] public string Name { get; private set; }
    [field: Header("설명")]
    [field: SerializeField] public string Desc { get; private set; }
    [field: Header("아이콘")]
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: Header("효과 - Key")]
    [field: SerializeField] public string EN { get; private set; }
    [field: Header("효과 - Value")]
    [field: SerializeField] public string Value { get; private set; }
    [field: Header("적용 대상")]
    [field: SerializeField] public EffectTargetType TargetType { get; private set; }
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Data/Item")]
public class ItemSO : ScriptableObject
{
    [field: Header("ID")]
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public EffectInfo EffectInfo { get; private set; }
}
