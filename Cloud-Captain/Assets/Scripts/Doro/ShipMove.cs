using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

	public bool reachedTarget;
	private bool isTargetObject;

	private GameObject targetObject;
	private Rigidbody rigBody;

	// values from settings
	private Vector3 currentPosition;
	public Vector3 targetPosition = new Vector3 (0,0,0);
	public float standardRange = 2;
	public float speed = 5;

	private float usedRange;

    public float RangeForSettleShips = 30;

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

			//float count = gameObject.GetComponent<PlayerManager> ().selectedUnits.Count;
			//Debug.Log (count);
			move ();

		}

		if (!reachedTarget && isTargetObject) {

			//float count = gameObject.GetComponent<PlayerManager> ().selectedUnits.Count;
			//Debug.Log (count);

			if (targetObject == null) {
				isTargetObject = false;
				targetPosition = currentPosition;
				//?? reachedTarget = true;
			} else {
				targetPosition = targetObject.transform.position;
				targetPosition.y = Setting.SHIP_FLIGHT_HEIGHT;
				move ();
			}

		}

    }

	public void stopShip (){
		reachedTarget = true;
	}

	public void startShip(){
		reachedTarget = false;
	}

	public void moveShip (Vector3 target)
	{
		reachedTarget = false;
		isTargetObject = false;

		targetPosition = target;
        targetPosition.y = Setting.SHIP_FLIGHT_HEIGHT;

		usedRange = standardRange;
    }

	public void moveShip (GameObject obj)
	{
		reachedTarget = false;
		isTargetObject = true;

		targetObject = obj;
		targetPosition = obj.transform.position;
		targetPosition.y = Setting.SHIP_FLIGHT_HEIGHT;

        if (targetObject.name == "SettleShip")
            usedRange = RangeForSettleShips;
        else
            usedRange = targetObject.GetComponent<BulletSpawnerReference> ().Attacker.GetAttackRange() - standardRange;

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

				reachedTarget = true;
				rigBody.angularVelocity = new Vector3 (0, 0, 0);

				//rigBody.isKinematic = true;

			}
		
	}

	bool isInRangeX (){

		return ( (currentPosition.x > targetPosition.x-usedRange) && (currentPosition.x < targetPosition.x+usedRange) );

	}

	bool isInRangeZ (){
		
		return ( (currentPosition.z > targetPosition.z-usedRange) && (currentPosition.z < targetPosition.z+usedRange) );

    }


	void OnCollisionEnter(Collision collision) {

		rigBody.MovePosition(transform.position + transform.right * Time.deltaTime * speed );

	}

	void OnCollisionExit(Collision collision) {

		rigBody.velocity = new Vector3(0,0,0);

	}

}
