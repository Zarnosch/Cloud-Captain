using UnityEngine;
using System.Collections;

public class CloudCamFollow : MonoBehaviour {

    public Camera Cam;

    private float yVal;

    void Start()
    {
        yVal = gameObject.transform.position.y;
    }

	// Update is called once per frame
	void Update ()
    {
        if(Cam)
            gameObject.transform.position = new Vector3(Cam.gameObject.transform.position.x, yVal, Cam.gameObject.transform.position.z);


        
    }
}
