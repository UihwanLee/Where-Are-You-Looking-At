using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdvisorType
{
    Fire,
    Water,
    Basic,
}

[CreateAssetMenu(fileName = "NewAdvisor", menuName = "Data/Advisor")]
public class AdvisorSO : ScriptableObject
{
    [field: Header("ID")]
    [field: SerializeField] public int ID { get; private set; }
    [field: Header("이름")]
    [field: SerializeField] public string Name { get; private set; }
    [field: Header("설명")]
    [field: SerializeField] public string Desc { get; private set; }
    [field: Header("아이콘")]
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: Header("타입")]
    [field: SerializeField] public AdvisorType Type { get; private set; }
    [field: Header("스탯 정보")]
    [field: SerializeField] public AdvisorStatSO StatSO { get; private set; }
}
