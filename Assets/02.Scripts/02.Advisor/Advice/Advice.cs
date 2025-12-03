using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Advice : ISellable
{
    private AdviceSO data;

    public Advice(AdviceSO data)
    {
        this.data = data;
    }

    public abstract void Apply(IAdviceReceiver applicator);

    public string GetName()
    {
        return data.Name;
    }

    public string GetDescription()
    {
        return data.Desc;
    }

    string ISellable.GetType()
    {
        return "Advice";
    }

    public Sprite GetSpriteIcon()
    {
        return data.Icon;
    }

    #region 프로퍼티

    public AdviceSO Data { get { return data; } }

    #endregion
}
