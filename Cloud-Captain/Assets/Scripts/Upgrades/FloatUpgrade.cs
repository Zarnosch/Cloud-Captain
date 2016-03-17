using UnityEngine;
using System.Collections;
using System;

public class FloatUpgrade : ValueUpgrade<float>
{
    private float deltaVal;


    protected override void OnLevelUp()
    {
        this.value += deltaVal;
    }


}
