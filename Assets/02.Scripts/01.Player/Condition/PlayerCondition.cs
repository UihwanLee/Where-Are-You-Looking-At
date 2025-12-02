using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : BaseCondition
{
    [SerializeField] private Attribute maxExp;
    [SerializeField] private Attribute exp;
    [SerializeField] private Attribute gold;

    protected override void Initialize()
    {
        base.Initialize();

        conditionDict.Add(AttributeType.MaxExp, maxExp);
        conditionDict.Add(AttributeType.Exp, exp);
        conditionDict.Add(AttributeType.Gold, gold);
    }

    public override void Add(AttributeType type, float amount)
    {
        if (conditionDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.AddValue(amount);

            // UI event Invoke
        }
    }

    public override void Sub(AttributeType type, float amount)
    {
        if (conditionDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.SubValue(amount);

            // UI event Invoke
        }
    }

    public override void Set(AttributeType type, float amount)
    {
        if (conditionDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.SetValue(amount);

            // UI event Invoke
        }
    }
}
