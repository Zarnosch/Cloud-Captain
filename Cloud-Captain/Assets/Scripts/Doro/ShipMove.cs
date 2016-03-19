using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

	[ReadOnly]
	public bool reachedTarget;
	private bool isTargetObject;

	private GameObject targetObject;
	private Rigidbody rigBody;

	// values from settings
	private Vector3 currentPosition;
	public Vector3 targetPosition = new Vector3 (0,0,0);
	public float range = 1;
	public float speed = 5;
	public float shipHighY = 5;

	// Use this for initialization
	void Start () {

		reachedTarget = true;

		targetObject = null;
		isTargetObject = false;

		rigBody = gameObject.GetComponent<Rigidbody> ();
		rigBody.velocity = new Vector3(0,0,0);
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate (){

		if (!reachedTarget && !isTargetObject) {
			move ();
		}

		if (!reachedTarget && isTargetObject) {
			if (targetObject == null) {
				isTargetObject = false;
				targetPosition = currentPosition;
			} else {
				targetPosition = targetObject.transform.position;
				targetPosition.y = shipHighY;
				move ();
			}
		}

    }


	public void moveShip (Vector3 target)
	{
		reachedTarget = false;
		isTargetObject = false;

		targetPosition = target;
        targetPosition.y = shipHighY;
    }

	public void moveShip (GameObject obj)
	{
		reachedTarget = false;
		isTargetObject = true;

		targetObject = obj;
		targetPosition = obj.transform.position;
		targetPosition.y = shipHighY;
	}

    void move(){
		
		//rigBody.isKinematic = false;
        
		currentPosition = rigBody.transform.position;

			// currentPosition is NOT in the range of the targetPositon
			if (!isInRangeX () || !isInRangeZ ()) {

				gameObject.transform.LookAt (targetPosition);
				rigBody.MovePosition (transform.position + transform.forward * Time.deltaTime * speed);

			}

			// currentPosition is in the range of the targetPosition
			if (isInRangeX () && isInRangeZ ()) { 
				Debug.Log ("Range");
				reachedTarget = true;

				rigBody.angularVelocity = new Vector3 (0, 0, 0);
				//rigBody.isKinematic = true;
			}
		
	}

	bool isInRangeX (){

		return ( (currentPosition.x > minRange.x) && (currentPosition.x < maxRange.x) );

	}

	bool isInRangeZ (){
		
        return ( (currentPosition.z > minRange.z) && (currentPosition.z < maxRange.z) );

    }


	void OnCollisionEnter(Collision collision) {

		rigBody.MovePosition(transform.position + transform.right * Time.deltaTime * speed );

	}

	void OnCollisionExit(Collision collision) {

		rigBody.velocity = new Vector3(0,0,0);

	}

}
