using UnityEngine;
using System.Collections;

public class GameObjectType : MonoBehaviour
{
	public Setting.ObjectType ObjectType;

    [HideInInspector]
    public bool paidSupplyCost;

    void Start()
    {
        PlayerManager.Instance.AddUnit(this, paidSupplyCost);
    }


    void OnDestroy()
    {
        PlayerManager.Instance.RemoveUnit(this);
    }
}
