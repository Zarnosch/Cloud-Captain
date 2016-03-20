using UnityEngine;
using System.Collections;

public class Methods : MonoBehaviour {

    public GameObject PlayerSelectItem;
    public GameObject EnemySelectItem;

    private int selectedHighlight;

    PlayerManager playerInstance;
    void Start () {
        playerInstance = PlayerManager.Instance;
        selectedHighlight = LayerMask.NameToLayer("SelectChooser");
    }


    public void SelectedListAdd(GameObject g)
    {
        GameObject newInstance = Instantiate(PlayerSelectItem, g.transform.position, Quaternion.identity) as GameObject;
        newInstance.transform.parent = g.transform;

		PlayerManager.Instance.UIManager.OpenPanelForObject (g);

        playerInstance.selectedUnits.Add(g);
    }

    public void SelectedListClear()
    {
        foreach (var item in playerInstance.selectedUnits)
        {
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
