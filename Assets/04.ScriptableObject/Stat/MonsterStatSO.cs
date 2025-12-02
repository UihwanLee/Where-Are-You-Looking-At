using UnityEngine;

[CreateAssetMenu(fileName = "newStat", menuName = "Stat/MonsterStat")]
public class MonsterStatSO : ScriptableObject
{
    [field: Header("HP")]
    [field: SerializeField] public float MaxHp { get; private set; }
    [field: Header("이동속도")]
    [field: SerializeField] public float Speed { get; private set; }
    [field: Header("데미지")]
    [field: SerializeField] public float Atk { get; private set; }
    [field: Header("스폰 속도")]
    [field: SerializeField] public float SpawnSpeed { get; private set; }
}
