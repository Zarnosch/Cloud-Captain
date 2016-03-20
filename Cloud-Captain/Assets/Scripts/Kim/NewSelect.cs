using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class NewSelect : MonoBehaviour
{
	public float selectorPlaneHeight = 56;
    public float flaechenInhaltFuerEinfachenKlick = 5;
	public float selectionHeightInEachDirection = 50;

	//public string[] rayMask = {"SelectPlane", "Buildings","Ships","Buildingsite", "Towersite", "SettlementSite"};
	[ReadOnly]
	public string[] rayMask;
	private int rayMaskInInt;

	public List<GameObject> buildingList = new List<GameObject>();
	public List<GameObject> islandList = new List<GameObject>();
	public List<GameObject> shipList = new List<GameObject>();

	public bool newListEntry = false;
    public bool newListIsEmpty = false;

	public GameObject figurePrefab;
	GameObject figurePrefabClone;

	public GameObject preFigurePrefab;
	GameObject preFigurePrefabClone;

	private bool mousePressed;
	private Vector3 first = new Vector3 (0, 0, 0);
	private Collider firstRay = null;

	private int building;
	private int ship;
	private int island;

	private int selectPlane;

    public Methods methods;



    // Use this for initialization
    void Start ()
	{

        methods = gameObject.GetComponent<Methods>();

		selectPlane = LayerMask.NameToLayer ("SelectPlane");

		building = LayerMask.NameToLayer ("Buildings");
		ship = LayerMask.NameToLayer ("Ships");
		island = LayerMask.NameToLayer ("Islands");

		rayMask = new string[]{"SelectPlane", "Buildings","Ships","Buildingsite", "Towersite", "SettlementSite"};
		rayMaskInInt = (LayerMask.GetMask (rayMask));
		//Debug.Log (rayMaskInInt);
	}

	void ErstelleRect (Vector3 f, Vector3 s)
	{
		float x, y, z;
		if (f.x < 0 && s.x > 0) {
			x = (s.x - f.x) / 2 + f.x;
		} else if (f.x > 0 && s.x < 0) {
			x = (f.x - s.x) / 2 + s.x;
		} else
			x = (f.x + s.x) / 2;
		y = f.y;
		if (f.z < 0 && s.z > 0) {
			z = (s.z - f.z) / 2 + f.z;
		} else if (f.z > 0 && s.z < 0) {
			z = (f.z - s.z) / 2 + s.z;
		} else
			z = (f.z + s.z) / 2;
        float flaechenInhalt = Mathf.Abs(f.x - s.x) * Mathf.Abs(f.z - s.z);

        // Here the raycast for selecting a single object is thrown (e.g. for the "sites")
        if (flaechenInhalt < flaechenInhaltFuerEinfachenKlick)
        {


			if (firstRay == null || firstRay.transform.gameObject.layer == selectPlane) {
				if (!EventSystem.current.IsPointerOverGameObject()) {
					PlayerManager.Instance.UIManager.HidePanel ();	
					methods.SelectedListClear();
					//Debug.Log (firstRay.transform.gameObject);
				}
			}
			else
            {
                //Debug.Log (LayerMask.LayerToName(firstRay.transform.gameObject.layer));
                if (firstRay.transform.gameObject.tag != "Enemy")
                {
                    methods.SelectedListClear();
                    methods.SelectedListAdd(firstRay.transform.gameObject);
                }
            }
            return;
                
        }
		Vector3 mittelwert = new Vector3 (x, y, z);
		Vector3 scaleVector = new Vector3 (Mathf.Abs (f.x - s.x), selectionHeightInEachDirection, Mathf.Abs (f.z - s.z));
		//figurePrefabClone = Instantiate (figurePrefab, mittelwert, Quaternion.identity) as GameObject;
  //      figurePrefabClone.transform.localScale = scaleVector;
  //      figurePrefabClone.gameObject.layer = building;
  //      figurePrefabClone.GetComponent<SelectScript>().newSelect = this;
        //Destroy (figurePrefabClone, 0.5f);

  //      figurePrefabClone = Instantiate (figurePrefab, mittelwert, Quaternion.identity) as GameObject;
		//figurePrefabClone.transform.localScale = scaleVector;
		//figurePrefabClone.gameObject.layer = island;
		//figurePrefabClone.GetComponent<SelectScript> ().newSelect = this;
		//Destroy (figurePrefabClone, 0.5f);

		figurePrefabClone = Instantiate (figurePrefab, mittelwert, Quaternion.identity) as GameObject;
		figurePrefabClone.transform.localScale = scaleVector;
		figurePrefabClone.gameObject.layer = selectPlane;
		figurePrefabClone.GetComponent<SelectScript> ().newSelect = this;
		//Destroy (figurePrefabClone, 0.5f);
	}

	void ZieheRect (Vector3 f, Vector3 s)
	{
		float x, y, z;
		if (f.x < 0 && s.x > 0) {
			x = (s.x - f.x) / 2 + f.x;
		} else if (f.x > 0 && s.x < 0) {
			x = (f.x - s.x) / 2 + s.x;
		} else
			x = (f.x + s.x) / 2;
		y = f.y+selectorPlaneHeight;
		if (f.z < 0 && s.z > 0) {
			z = (s.z - f.z) / 2 + f.z;
		} else if (f.z > 0 && s.z < 0) {
			z = (f.z - s.z) / 2 + s.z;
		} else
			z = (f.z + s.z) / 2;
		Vector3 mittelwert = new Vector3 (x, y, z);
		preFigurePrefabClone = Instantiate (preFigurePrefab, mittelwert, Quaternion.identity) as GameObject;
		//Destroy (figurePrefabClone, 3);
		Vector3 scaleVector = new Vector3 (Mathf.Abs (f.x - s.x), 0.2f, Mathf.Abs (f.z - s.z));
		preFigurePrefabClone.transform.localScale = scaleVector;
	}



	//if (item.collider.name == "Plane")

	// Update is called once per frame
	void Update ()
	{
		if (newListEntry) {
            if (shipList.Count > 0) {

                methods.SelectedListClear();
                foreach (var item in shipList)
                {
                    methods.SelectedListAdd(item);
                }

                shipList.Clear();
                islandList.Clear ();
                buildingList.Clear();
            }
            else if (islandList.Count > 0)
            {
                methods.SelectedListClear();
                methods.SelectedListAdd(islandList[0]);

                //shipList.Clear();
                islandList.Clear();
                buildingList.Clear();
            }
            else if (buildingList.Count > 0)
            {
                methods.SelectedListClear();
                methods.SelectedListAdd(buildingList[0]);

                //shipList.Clear();
                //islandList.Clear();
                buildingList.Clear();
            }
            newListEntry = false;
		}

        if (newListIsEmpty)
        {
            if(methods)
                methods.SelectedListClear();
            newListIsEmpty = false;
        }

        if (Input.GetMouseButton (0)) {
			if (mousePressed) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				Vector3 second = new Vector3 (0, 0, 0);
				RaycastHit[] hits = Physics.RaycastAll (ray);
				foreach (var item in hits) {					
					if (item.transform.gameObject.layer == selectPlane)
						second = item.point;
				}
				ZieheRect (first, second);
			}
		}

		//if (Input.GetMouseButtonDown (0)) {

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            //Debug.Log (ray);

            // Single RayCast
            RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 1000f, rayMaskInInt))
            {
                firstRay = hit.collider;
				//Debug.Log (firstRay.transform.gameObject.name);
            }

            RaycastHit[] hits = Physics.RaycastAll (ray);
			foreach (var item in hits) {
				if (item.transform.gameObject.layer == selectPlane)
					first = item.point;
			}
			mousePressed = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector3 second = new Vector3 (0, 0, 0);
			RaycastHit[] hits = Physics.RaycastAll (ray);
			foreach (var item in hits) {
				if (item.transform.gameObject.layer == selectPlane)
					second = item.point;
			}
			// Testing for Rect Size
			ErstelleRect (first, second);

			mousePressed = false;
		}



		//				if (item.collider.tag == "Respawn") {
		//					figurePrefabClone = Instantiate (figurePrefab, item.point, Quaternion.identity) as GameObject;
		//					Destroy (figurePrefabClone, 3);
		//				}


		//			RaycastHit hit;
		//			if (Physics.Raycast (ray, out hit)) {
		//				Debug.Log (hit.point);
		//				if (hit.collider.tag == "Finish") {
		//					//Debug.Log ("Figure");
		//					bool s = hit.collider.GetComponent<AnimationBehaviour>().shaking;
		//					hit.collider.GetComponent<Animator>().SetBool ("Shaking", !s);
		//
		//				}
		//				if (hit.collider.tag == "Respawn") {
		//					Debug.Log ("Plane");
		//
		//				}
		//			}

		//		}
		//		if (Input.GetMouseButtonDown (0)) {
		//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//			RaycastHit hit;
		//			if (Physics.Raycast (ray, out hit)) {
		//				Debug.Log (hit.point);
		//				if (hit.collider.tag == "Finish") {
		//					//Debug.Log ("Figure");
		//					//hit.collider.GetComponent<AnimationBehaviour>().shaking = true;
		//					hit.collider.GetComponent<Animator>().SetBool ("Shaking", true);  
		//
		//				}
		//				if (hit.collider.tag == "Respawn") {
		//					Debug.Log ("Plane");
		//
		//				}
		//			}
		//		}
	}
}



