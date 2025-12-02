using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerkTargetType
{
    Player,
    Advisor,
}

[CreateAssetMenu(fileName = "NewPerk", menuName = "Data/Perk")]
public class PerkSO : ScriptableObject
{
    [field: Header("ID")]
    [field: SerializeField] public int ID { get; private set; }
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
    [field: SerializeField] public PerkTargetType TargetType { get; private set; }
}
