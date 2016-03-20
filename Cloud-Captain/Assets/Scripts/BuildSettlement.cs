using UnityEngine;
using System.Collections;

public class BuildSettlement : MonoBehaviour
{

    public bool TryPlaceSettlement(IslandManager island)
    {
        if (!island.Nexus)
        {
            GameObject where = island.GetSettlementSite();
            if (where)
            {
                float dist = (where.transform.position - gameObject.transform.position).magnitude;

                if (dist < Setting.MAX_RANGE_SETTLESHIP)
                {
                    GameObject settlement = BuildManager.Instance.TryPlaceBuilding(BuildManager.BuildingObject.Settlement, where.transform);
                    island.AddBuilding(settlement, where);
                    Destroy(gameObject);

                    return true;
                }
            }
        }

        return false;
    }


}
