using UnityEngine;
using System.Collections;

public class UpgradeToNexus : MonoBehaviour {



    public bool TryUpgrade()
    {
        if (BuildManager.Instance.TryPlaceBuilding(BuildManager.BuildingObject.Nexus, gameObject.transform))
        {

            DeleteMe();
            return true;
        }

        return false;
    }


    public void UpgradeNoCost()
    {
        DeleteMe();
        BuildManager.Instance.TryPlaceBuildingNoCost(BuildManager.BuildingObject.Nexus, gameObject.transform);
    }

    private void DeleteMe()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpgradeNoCost();
        }
    }




}
