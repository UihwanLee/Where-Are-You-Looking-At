using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseCondition: 사용 객체: 몬스터, 플레이어
/// </summary>
public class BaseCondition : MonoBehaviour
{
    [SerializeField] protected Attribute maxHp;
    [SerializeField] protected Attribute hp;

    protected Dictionary<AttributeType, Attribute> conditionDict;

    protected void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        // 100 -> SO나 Json으로 설정해줘야함
        maxHp = new Attribute(0, 100);
        hp = new Attribute(1, maxHp.Value);

        conditionDict = new Dictionary<AttributeType, Attribute>();
        conditionDict.Add(AttributeType.Hp, hp);
        conditionDict.Add(AttributeType.MaxHp, maxHp);
    }

    public virtual void Add(AttributeType type, float amount)
    {
        if (conditionDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.AddValue(amount);
        }
    }

    public virtual void Sub(AttributeType type, float amount)
    {
        if (conditionDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.SubValue(amount);
        }
    }

    public virtual void Set(AttributeType type, float amount)
    {
        if (conditionDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.SetValue(amount);
        }
    }

    public virtual void TakeDamage(Transform other, float damage, Color? color = null)
    {
        // damage Text 표시
        FloatingTextPoolManager.Instance.SpawnText(TextType.UNDEF, damage.ToString(), this.transform, color);

        // damage 적용
        Sub(AttributeType.Hp, damage);
    }
}
