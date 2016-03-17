using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {

	BoxCollider box;
	bool mousePressed;
	Vector3 first;
	// Use this for initialization
	void Start () {
		box = gameObject.GetComponentInParent<BoxCollider> ();
		mousePressed = false;
		first = new Vector3 (0, 0, 0);

	}
	
	// Update is called once per frame
	void Update () {




		box.enabled = false;
		if (Input.GetKeyDown ("d")) {
			box.enabled = true;
			//gameObject.GetComponentInParent<BoxCollider>().enabled
		}

	}

	void OnTriggerStay(Collider coll){
		Debug.Log (coll.gameObject.tag);

	}

	void ErstelleRect(Vector3 f, Vector3 s){
		float x, y, z;
		if (f.x < 0 && s.x > 0) {
			x = (s.x - f.x) / 2 + f.x;
		}
		if (f.x > 0 && s.x < 0) {
			x = (f.x - s.x) / 2 + s.x;
		}
		y = f.y;
		if (f.z < 0 && s.z > 0) {
			z = (s.z - f.z) / 2 + f.z;
		}
		if (f.z > 0 && s.z < 0) {
			z = (f.z - s.z) / 2 + s.z;
		}
	}
}
