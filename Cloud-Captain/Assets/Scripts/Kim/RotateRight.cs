using UnityEngine;
using System.Collections;

public class RotateRight : MonoBehaviour {

    private Vector3 startVector = new Vector3(90, 0, 0);
    private Vector3 addVector = new Vector3(0, 0.4f, 0);
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        startVector += addVector;
        if (startVector.y > 360)
            startVector.y -= 360;
        //gameObject.transform.Rotate(Vector3.right * Time.deltaTime * 200);
        gameObject.transform.localEulerAngles = startVector;
    }
}
