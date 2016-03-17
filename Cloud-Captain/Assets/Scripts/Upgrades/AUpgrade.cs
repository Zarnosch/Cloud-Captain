using UnityEngine;
using System.Collections;
using System;


public abstract class AUpgrade : MonoBehaviour
{
    public string UpgradeName;
    protected int level = 0;

    public void LevelUp()
    {
        level++;
        OnLevelUp();
    }

    public int GetLevel()
    {
        return level;
    }

    protected abstract void OnLevelUp();

}
