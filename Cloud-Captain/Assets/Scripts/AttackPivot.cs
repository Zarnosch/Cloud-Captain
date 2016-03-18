using UnityEngine;
using System.Collections;

public class AttackPivot : MonoBehaviour {

    public GameObject Pivot;

    void Start()
    {
        if (!Pivot)
            Pivot = gameObject;
    }
	
}
