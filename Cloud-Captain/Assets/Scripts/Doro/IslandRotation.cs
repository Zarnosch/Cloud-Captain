using UnityEngine;
using System.Collections;

public class IslandRotation : MonoBehaviour {

	public float speed;

	private float minSpeed;
	private float maxSpeed;

	private float wheelMove;
	private Quaternion moveRotation;
	private Rigidbody rigBody;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate (){

		islandRotate ();

	}

	void islandRotate() {

		rigBody = gameObject.GetComponent<Rigidbody> ();

		if (Input.GetKey ("left ctrl")) {

			wheelMove = Input.GetAxis ("Mouse ScrollWheel");
			moveRotation = gameObject.transform.rotation;

				//gameObject.transform.Rotate (Vector3.up * Time.deltaTime * wheelMove * speed);
				//rigBody.AddForce( Vector3.up * wheelMove * speed, ForceMode.Acceleration);

			rigBody.AddTorque( Vector3.up * wheelMove * speed, ForceMode.Acceleration);

		}

	}
}
