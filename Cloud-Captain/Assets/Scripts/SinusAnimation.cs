using UnityEngine;
using System.Collections;

public class SinusAnimation : MonoBehaviour {

    public float speed = 1.0f;
    public float heightFactor = 1.0f;
    private float time = 0.0f;
    private Vector3 startPos;


    void Start()
    {
        startPos = gameObject.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime * speed;

        //scale for [0.0f, 1.0f]
        float sin = Mathf.Sin(time) * 0.5f + 0.5f;

        gameObject.transform.localPosition = startPos + new Vector3(0.0f, sin * heightFactor, 0.0f);
	}
}
