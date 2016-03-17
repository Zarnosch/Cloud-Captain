using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

	private Vector3 minRange;
	private Vector3 maxRange;

	private Rigidbody rigBody;

	// values from settings
	private Vector3 m_currentPosition;
	private Vector3 m_targetPosition;
	private float m_range;
	private float m_speed;

	// properties
	public Vector3 M_currentPosition { get; set; }
	public Vector3 M_targetPosition { get; set; }
	public float M_range {
		get {
			return m_range;
		}
		set {
			m_range = value;
			minRange = new Vector3 (m_targetPosition.x - m_range, 0, m_targetPosition.z - m_range);
			maxRange = new Vector3 (m_targetPosition.x + m_range, 0, m_targetPosition.z + m_range);
		}
	}
	public float M_speed{ get; set; }


	// Use this for initialization
	void Start () {

		//------Testing------

		m_speed = 10f;
		m_targetPosition = new Vector3 (20, 0, 20);
		m_range = 5f;

		//----------------------

		minRange = new Vector3 (m_targetPosition.x - m_range, 0, m_targetPosition.z - m_range);
		maxRange = new Vector3 (m_targetPosition.x + m_range, 0, m_targetPosition.z + m_range);

		rigBody = gameObject.GetComponent<Rigidbody> ();
		rigBody.velocity = new Vector3(0,0,0);
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate (){

		moveShip (m_targetPosition);

	}

	void moveShip(Vector3 targetPosition){

		rigBody.isKinematic = false;
		m_targetPosition = targetPosition;
		m_currentPosition = rigBody.transform.position;

		// currentPosition is NOT in the range of the targetPositon
		if ( !isInRangeX() || !isInRangeZ() ) {

			gameObject.transform.LookAt (m_targetPosition);
			rigBody.MovePosition(transform.position + transform.forward * Time.deltaTime * m_speed );

		}

		// currentPosition is in the range of the targetPosition
		if ( isInRangeX() && isInRangeZ() ) { 

			rigBody.angularVelocity = new Vector3(0,0,0);
			rigBody.isKinematic = true;

		}

	}

	bool isInRangeX (){
		return ( (m_currentPosition.x > minRange.x) && (m_currentPosition.x < maxRange.x) );
	}

	bool isInRangeZ (){
		return ( (m_currentPosition.z > minRange.z) && (m_currentPosition.z < maxRange.z) );
	}


	void OnCollisionEnter(Collision collision) {

		rigBody.MovePosition(transform.position + transform.right * Time.deltaTime * m_speed );

	}

	void OnCollisionExit(Collision collision) {

		rigBody.velocity = new Vector3(0,0,0);

	}

}
