using UnityEngine;
using System.Collections;

public class SelectScript : MonoBehaviour {


	private int frameCounter = 0;
	// Objects lives for 'frameCounterLimit' frames
	private int frameCounterLimit = 3;

	private BoxCollider box;
	//GameObject parent;

	private int building;
	private int ship;
	private int island;

	public NewSelect newSelect;


	// Use this for initialization
	void Start () {
//		box = gameObject.GetComponent<BoxCollider> ();
		//newSelect = gameObject.GetComponentInParent<NewSelect>();
		building = LayerMask.NameToLayer ("Buildings");
		ship = LayerMask.NameToLayer ("Ships");
		island = LayerMask.NameToLayer ("Islands");
	}
	
	// Update is called once per frame
	void Update () {
		if(frameCounter>frameCounterLimit)
			Destroy (gameObject);
		frameCounter++;
//		box.enabled = true;
//		if (Input.GetKeyDown ("d")) {
//			box.enabled = true;
			//gameObject.GetComponentInParent<BoxCollider>().enabled

	}

	void OnTriggerStay(Collider coll){

		if (coll.gameObject.layer == building) {
			newSelect.buildingList.Add (coll.gameObject);
			newSelect.newListEntry = true;
		}
		else if (coll.gameObject.layer == island) {
			newSelect.islandList.Add (coll.gameObject);
			newSelect.newListEntry = true;
		}
		else if (coll.gameObject.layer == ship) {
			newSelect.shipList.Add (coll.gameObject);
			newSelect.newListEntry = true;
		}

//		Debug.Log(coll.gameObject.layer);
//		Debug.Log (newSelect);
		Destroy (gameObject);

	}

}
