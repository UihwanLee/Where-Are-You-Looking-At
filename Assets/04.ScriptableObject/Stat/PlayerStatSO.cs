using UnityEngine;

[CreateAssetMenu(fileName = "newStat", menuName = "Stat/PlayerStat")]
public class PlayerStatSO : ScriptableObject
{
    [field: Header("최대 HP")]
    [field: SerializeField] public float MaxHp { get; private set; }
    [field: Header("HP 재생")]
    [field: SerializeField] public float ReproductionHp { get; private set; }
    [field: Header("HP 회복")]
    [field: SerializeField] public float RecoveryHp { get; private set; }
    [field: Header("방어")]
    [field: SerializeField] public float Defense { get; private set; }
    [field: Header("회피")]
    [field: SerializeField] public float Evasion { get; private set; }
    [field: Header("속도(이동 속도)")]
    [field: SerializeField] public float Speed { get; private set; }
    [field: Header("행운")]
    [field: SerializeField] public float Luck { get; private set; }
    [field: Header("아이템 흡수")]
    [field: SerializeField] public float AbsorptionItem { get; private set; }
}
