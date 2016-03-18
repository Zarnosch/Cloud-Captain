using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float Speed;
    public Vector3 Axis;


	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Rotate(Axis, Speed * Time.deltaTime, Space.Self);
        
	}
}
