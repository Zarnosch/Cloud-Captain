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


    private void DeleteMe()
    {
        Destroy(gameObject);
    }





}
