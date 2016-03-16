using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
    public static PlayerManager Instance { get; private set; }

    public List<GameObject> selectedUnits;

	// Use this for initialization
	void Start () 
    {
        Debug.Assert(!Instance);
       
        Instance = this;
	}
	

}
