using UnityEngine;
using System.Collections;

public class GameobjectType : MonoBehaviour {
	
	public Setting.ObjectType ObjectType;


    void Start()
    {
        PlayerManager.Instance.AddBuiltObject(this);
    }

}
