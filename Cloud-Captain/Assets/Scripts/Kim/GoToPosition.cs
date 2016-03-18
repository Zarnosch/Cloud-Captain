using UnityEngine;
using System.Collections;

public class GoToPosition : MonoBehaviour {


    private int selectPlane;
    PlayerManager playerInstance;
    void Start () {
        selectPlane = LayerMask.NameToLayer("SelectPlane");
        playerInstance = PlayerManager.Instance;
    }

    void Update()
    {

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (var item in hits)
            {
                if (item.transform.gameObject.layer == selectPlane)
                {
                    //Debug.Log(item.point);
                    if (playerInstance.selectedUnits.Count > 0)
                    {
                        foreach (var selected in playerInstance.selectedUnits)
                        {
							Debug.Log (item.point);
							selected.gameObject.GetComponent<ShipMove>().moveShip(item.point);
                            //selected.gameObject.GetComponent<ShipMove>().moveShip(new Vector3(20,20,20));
                        }
                    }
                }

            }

        }
    }
}
