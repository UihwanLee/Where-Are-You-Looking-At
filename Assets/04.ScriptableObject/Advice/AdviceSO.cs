using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdviceTargetType
{
    Player,
    Advisor,
}

[CreateAssetMenu(fileName = "NewAdvice", menuName = "Data/Advice")]
public class AdviceSO : ScriptableObject
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
    [field: Header("효과 대상")]
    [field: SerializeField] public AdviceTargetType TargetType { get; private set; }
}
