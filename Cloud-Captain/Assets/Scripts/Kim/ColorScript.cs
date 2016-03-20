using UnityEngine;
using System.Collections;

public class ColorScript : MonoBehaviour {



	// Use this for initialization
	void Start () {
        Color boxColor = Color.green;
        boxColor.a = 0.1f;
        gameObject.GetComponent<Renderer>().material.color = boxColor;
        //gameObject.GetComponent<Renderer>().material.SetFloat
        //Debug.Log(gameObject.GetComponent<Renderer>().material.GetFloat("_Mode"));
        //Debug.Log(Shader.Find("Transparency"));
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Mode", 2);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
