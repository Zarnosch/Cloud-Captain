using UnityEngine;
using System.Collections;

public class UpgradeToNexus : MonoBehaviour {



    public bool TryUpgrade()
    {

        BuildBuildingFeedback result = BuildManager.Instance.TryPlaceBuilding(BuildManager.BuildingObject.Nexus, gameObject.transform);
        if (result.WasSuccessfull())
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
