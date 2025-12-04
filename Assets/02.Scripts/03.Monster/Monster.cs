using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterStat Stat { get; private set; }
    public MonsterController Controller { get; private set; }
    public BaseCondition Condition { get; private set; }

    private void OnDisable()
    {
        Condition.Set(AttributeType.Hp, Stat.MaxHp);
    }

    private void Awake()
    {
        Stat = GetComponent<MonsterStat>();
        Controller = GetComponent<MonsterController>();
        Condition = GetComponent<BaseCondition>();
    }
}
