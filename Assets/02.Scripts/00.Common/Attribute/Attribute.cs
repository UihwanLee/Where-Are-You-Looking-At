using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute
{
    [SerializeField] private int _localIndex;
    [SerializeField] private float _value;

    public Attribute(int localIndex, float value)
    {
        _localIndex = localIndex;
        _value = value;
    }

    public void AddValue(float amount)
    {
        _value += amount;
    }

    public void SubValue(float amount)
    {
        _value -= amount;
    }

    public void SetValue(float value)
    {
        _value = value;
    }

    #region 프로퍼티

    public int LocalIndex { get { return _localIndex; } }
    public float Value { get { return _value; } }

    #endregion
}
