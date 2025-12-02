using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute
{
    [SerializeField] private float _value;

    public Attribute(float value)
    {
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
    public float Value { get { return _value; } }

    #endregion
}
