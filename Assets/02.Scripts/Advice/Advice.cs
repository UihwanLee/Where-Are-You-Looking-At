using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Advice
{
    private AdviceSO data;

    public Advice(AdviceSO data)
    {
        this.data = data;
    }

    public abstract void Apply(IAdviceReceiver applicator);

    #region 프로퍼티

    public AdviceSO Data { get { return data; } }

    #endregion
}
