using UnityEngine;
using System.Collections;

public class ColorBlue : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
