using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {


	//-----------static
	//Edges for the world
	private float _worldEdgeLeft;
	private float _worldEdgeRight;
	private float _worldEdgeTop;
	private float _worldEdgeBottom;

	//Edges for starting movement
	private float _moveEdgeRight;
	private float _moveEdgeLeft;
	private float _moveEdgeTop;
	private float _moveEdgeBottom;

	private Vector2 _cameraRect; // x = Width, y = Height
	private Vector3 _cameraStartPos;
	private float _cameraMoveSpeed;	//maxspeed for camera movement
	private float _cameraEdgeSpeed; //how fast the speed increases at an edge


	//------------variable
	private Vector3 _mouseCurrentPos;
	private Vector3 _cameraCurrentPos;
	private Vector3 _cameraMovePos;

	private float _smoothSpeed;

	Camera camera;


	void Start () {
	
		//------------------------------- Values for testing
		Vector3 testStartPos = new Vector3( 0, 1, -10 );
		float testMaxRight = 10;
		float testMaxLeft = -10;
		float testMaxTop = 10;
		float testMaxBottom = -10;
		float testSpeed = 0.3f;
		float testEdgeSpeed = 0.5f;
		//--------------------------------

		camera = GetComponent<Camera> ();

		_cameraStartPos = testStartPos;

		_worldEdgeRight = testMaxRight;
		_worldEdgeLeft = testMaxLeft;
		_worldEdgeTop = testMaxTop;
		_worldEdgeBottom = testMaxBottom;

		_cameraRect.x = camera.pixelWidth;
		_cameraRect.y = camera.pixelHeight;

		//set edges starting movement
		_moveEdgeRight = _cameraRect.x * 0.95f;
		_moveEdgeLeft = _cameraRect.x * 0.05f;
		_moveEdgeTop = _cameraRect.y * 0.95f;
		_moveEdgeBottom = _cameraRect.y * 0.05f;

		//set Movementspeed (max value)
		_cameraMoveSpeed = testSpeed;
		_cameraEdgeSpeed = testEdgeSpeed;

		//set camera to startpos
		camera.transform.position = _cameraStartPos;

	}

	void Update () {

		_mouseCurrentPos = Input.mousePosition;
		_cameraCurrentPos = camera.transform.position;

		_cameraMovePos = _cameraCurrentPos;

		//move right
		if (_mouseCurrentPos.x >= _moveEdgeRight) {
			_smoothSpeed = (_moveEdgeRight - _mouseCurrentPos.x) *(-_cameraEdgeSpeed);
			_cameraMovePos.x += _smoothSpeed * _cameraMoveSpeed * Time.deltaTime;
		}

		//move left
		if (_mouseCurrentPos.x <= _moveEdgeLeft) {
			_smoothSpeed = (_mouseCurrentPos.x - _moveEdgeLeft) *(-_cameraEdgeSpeed);
			_cameraMovePos.x -= _smoothSpeed * _cameraMoveSpeed * Time.deltaTime;
		}

		//move up
		if (_mouseCurrentPos.y >= _moveEdgeTop) {
			_smoothSpeed = (_moveEdgeTop - _mouseCurrentPos.y) *(-_cameraEdgeSpeed);
			_cameraMovePos.z += _smoothSpeed * _cameraMoveSpeed * Time.deltaTime;
		}

		//move down
		if (_mouseCurrentPos.y <= _moveEdgeBottom) {
			_smoothSpeed = (_mouseCurrentPos.y - _moveEdgeBottom) *(-_cameraEdgeSpeed);
			_cameraMovePos.z -= _smoothSpeed * _cameraMoveSpeed * Time.deltaTime;
		}

		// proof worldedges
		if (_cameraMovePos.x <= _worldEdgeRight && _cameraMovePos.x >= _worldEdgeLeft && _cameraMovePos.z <= _worldEdgeTop && _cameraMovePos.z >= _worldEdgeBottom) {
			camera.transform.position = _cameraMovePos;
		}


	}


}
