using UnityEngine;

[CreateAssetMenu(fileName = "newStat", menuName = "Stat/MonsterStat")]
public class MonsterStatSO : ScriptableObject
{
    [field: SerializeField] public float MaxHp { get; private set; } 
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float SpawnSpeed { get; private set; }
}
