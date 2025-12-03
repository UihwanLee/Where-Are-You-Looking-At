using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdvice : Advice
{
    public PlayerAdvice(AdviceSO data) : base(data)
    {
    }

    public override void Apply(IAdviceReceiver applicator)
    {
        Debug.Log("Advice 적용");
        applicator.ReceiveAdvice(this);
    }
}
