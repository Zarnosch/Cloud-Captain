using UnityEngine;
using System.Collections;

public class GoToPosition : MonoBehaviour {

	public string[] rayMask;
	private int rayMaskInInt;

	private int building;
	private int ship;
    private int selectPlane;
    PlayerManager playerInstance;
    void Start () {
        selectPlane = LayerMask.NameToLayer("SelectPlane");
		building = LayerMask.NameToLayer ("Buildings");
		ship = LayerMask.NameToLayer ("Ships");
        playerInstance = PlayerManager.Instance;

		rayMask = new string[]{"Buildings","Ships"};
		rayMaskInInt = (LayerMask.GetMask (rayMask));
    }

    void Update()
    {

        if (Input.GetMouseButtonUp(1))
        {
			if (playerInstance.selectedUnits.Count > 0) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll (ray);
				foreach (var item in hits) {

					if (item.transform.gameObject.tag == "Enemy") {
						if (playerInstance.selectedUnits [0].layer == ship) {
							
							foreach (var selected in playerInstance.selectedUnits) {
							
								// Single RayCast
								RaycastHit hit;

								if (Physics.Raycast (ray, out hit, 1000f, rayMaskInInt)) {
									//Debug.Log ("Test");
									selected.gameObject.GetComponent<ShipMove> ().moveShip (hit.collider.transform.gameObject);
									return;
									//Debug.Log (firstRay.transform.gameObject.name);
								}
								//selected.gameObject.GetComponent<ShipMove> ().moveShip (item.point);
								//selected.gameObject.GetComponent<ShipMove>().moveShip(new Vector3(20,20,20));
							}
						}
					}


				}
				foreach (var item in hits) {
					if (item.transform.gameObject.layer == selectPlane) {
						//Debug.Log(item.point);
						foreach (var selected in playerInstance.selectedUnits) {
							//Debug.Log (item.point);
							//Debug.Log(selected.name);
							selected.gameObject.GetComponent<ShipMove> ().moveShip (item.point);
							//selected.gameObject.GetComponent<ShipMove>().moveShip(new Vector3(20,20,20));
						}
					}
				}
			}
        }
    }
}
