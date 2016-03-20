using UnityEngine;
using System.Collections;

public class Methods : MonoBehaviour {

    public GameObject PlayerSelectItem;
    public GameObject EnemySelectItem;

    private int selectedHighlight;


    private int buildingSite;
    private int towerSite;
    private int settlementSite;

    private int ship;
    private int selectPlane;

    PlayerManager playerInstance;
    void Start () {
        playerInstance = PlayerManager.Instance;
        selectedHighlight = LayerMask.NameToLayer("SelectChooser");

        buildingSite = LayerMask.NameToLayer("Buildingsite");
        towerSite = LayerMask.NameToLayer("Towersite");
        settlementSite = LayerMask.NameToLayer("SettlementSite");

        ship = LayerMask.NameToLayer("Ships");
    }


    public void SelectedListAdd(GameObject g)
    {
        if(g.layer == buildingSite || g.layer == towerSite || g.layer == settlementSite)
        {
            GameObject newInstance = Instantiate(PlayerSelectItem, g.transform.position+new Vector3(0,0.6f,0), Quaternion.identity) as GameObject;
            newInstance.transform.parent = g.transform;

            PlayerManager.Instance.UIManager.OpenPanelForObject(g);

            playerInstance.selectedUnits.Add(g);
        }       
        else
        {
            if(g.layer == ship)
            {
                g.GetComponentInChildren<HoldSelectedEnemy>().isSelected = true;
                GameObject newInstance = Instantiate(PlayerSelectItem, g.transform.position, Quaternion.identity) as GameObject;
                newInstance.transform.parent = g.transform;

                PlayerManager.Instance.UIManager.OpenPanelForObject(g);

                playerInstance.selectedUnits.Add(g);
            }
            else
            {
                GameObject newInstance = Instantiate(PlayerSelectItem, g.transform.position, Quaternion.identity) as GameObject;
                newInstance.transform.parent = g.transform;

                PlayerManager.Instance.UIManager.OpenPanelForObject(g);

                playerInstance.selectedUnits.Add(g);
            }
            
        }
        
        
    }

    public void SelectedListClear()
    {
        foreach (var item in playerInstance.selectedUnits)
        {
            item.GetComponentInChildren<HoldSelectedEnemy>().isSelected = false;
            foreach (Transform kidlette in item.transform)
            {
                //print("kid: " + kidlette.name);
                //kidlette.position = room;
                //if(kidlette.gameObject.layer == "SelectRingPlayer(Clone)")
                if(kidlette.gameObject.layer == selectedHighlight)
                    Destroy(kidlette.gameObject);
            }
            //gameObject.GetComponentInChildren<DestroyThis>().DestroyThisObject();
        }
        playerInstance.selectedUnits.Clear();
    }

    void Update()
    {

        for (int i = playerInstance.selectedUnits.Count - 1; i >= 0; i--)
        {
            if (playerInstance.selectedUnits[i] == null)
            playerInstance.selectedUnits.RemoveAt(i);
        }
    }
        


}
