using UnityEngine;

[CreateAssetMenu(fileName = "newStat", menuName = "Stat/AdvisorStat")]
public class AdvisorStatSO : ScriptableObject
{
    [field: Header("공격력")]
    [field: SerializeField] public float Atk { get; private set; }
    [field: Header("공격 속도")]
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: Header("공격 사거리")]
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: Header("치명타 확률")]
    [field: SerializeField] public float CritChance { get; private set; }
    [field: Header("치명타 계수")]
    [field: SerializeField] public float CritMultiplier { get; private set; }
    [field: Header("투사체 속도")]
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
}
