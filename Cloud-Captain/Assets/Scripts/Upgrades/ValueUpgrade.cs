using UnityEngine;
using System.Collections;
using System;


public abstract class ValueUpgrade<T> : AUpgrade
{
    public T value;

    public T GetValue()
    {
        return value;
    }

    public void SetValue(T val)
    {
        this.value = val;
    }

    protected abstract override void OnLevelUp();


}
