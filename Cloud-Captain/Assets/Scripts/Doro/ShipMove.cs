using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

	public float speed = 5;
	public float range = 5;
	public Vector3 targetPosition;

	private Vector3 minRange;
	private Vector3 maxRange;
	private Vector3 oldTarget;
	[ReadOnly]
	public bool reachedTarget;
	private bool isTargetObject;

	private GameObject targetObject;
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

		m_speed = speed;
		m_targetPosition = new Vector3(0,0,0);
		m_range = range;

		minRange = new Vector3 (m_targetPosition.x - m_range, 0, m_targetPosition.z - m_range);
		maxRange = new Vector3 (m_targetPosition.x + m_range, 0, m_targetPosition.z + m_range);

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
			//moveShip(new Vector3 (-20,0,-10));
			move ();
		}

		if (!reachedTarget && isTargetObject) {
			if (targetObject == null) {
				isTargetObject = false;
				m_targetPosition = m_currentPosition;
			} else {
				m_targetPosition = targetObject.transform.position;
				m_targetPosition.y = Setting.SHIP_FLIGHT_HEIGHT;
				move ();
			}
		}

    }


	public void moveShip (Vector3 target)
	{
		reachedTarget = false;
		isTargetObject = false;
        //move (target);
		m_targetPosition = target;
        m_targetPosition.y = Setting.SHIP_FLIGHT_HEIGHT;
    }

	public void moveShip (GameObject obj)
	{
		reachedTarget = false;
		isTargetObject = true;
		targetObject = obj;
		m_targetPosition = obj.transform.position;
		m_targetPosition.y = Setting.SHIP_FLIGHT_HEIGHT;
	}

    void move(){
		
		//rigBody.isKinematic = false;
        
		//m_targetPosition = targetPosition;
		m_currentPosition = rigBody.transform.position;
		//m_targetPosition.y = shipHighY;

	
			// currentPosition is NOT in the range of the targetPositon
			if (!isInRangeX () || !isInRangeZ ()) {

				gameObject.transform.LookAt (m_targetPosition);
				rigBody.MovePosition (transform.position + transform.forward * Time.deltaTime * m_speed);

			}

			// currentPosition is in the range of the targetPosition
			if (isInRangeX () && isInRangeZ ()) { 
				//Debug.Log ("Range");
				reachedTarget = true;

				rigBody.angularVelocity = new Vector3 (0, 0, 0);
				//rigBody.isKinematic = true;

			}
		
	}

	bool isInRangeX (){

        //float xDist = targetPosition.x - gameObject.transform.position.x;

        //return xDist < range;

		return ( (m_currentPosition.x > m_targetPosition.x-range) && (m_currentPosition.x < m_targetPosition.x+range) );
		//return m_currentPosition.x < minRange.x;
	}

	bool isInRangeZ (){
		return ( (m_currentPosition.z > m_targetPosition.z-range) && (m_currentPosition.z < m_targetPosition.z+range) );
		//return m_currentPosition.z > minRange.z;

		//float zDist = targetPosition.z - gameObject.transform.position.z;

        //return zDist < range;
    }


	void OnCollisionEnter(Collision collision) {

		rigBody.MovePosition(transform.position + transform.right * Time.deltaTime * m_speed );

	}

	void OnCollisionExit(Collision collision) {

		rigBody.velocity = new Vector3(0,0,0);

	}

}
