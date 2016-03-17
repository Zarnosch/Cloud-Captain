using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BuildManager : MonoBehaviour
{
    public enum BuildObject { TeslaTower, ArtilleryTower, PowerPlant, Mine, Workshop, Shipyard, Nexus, Settlement }
    public static BuildManager Instance { get; private set; }

    #region Building prefabs

    [SerializeField]
    private GameObject TeslaTowerPrefab;
    [SerializeField]
    private GameObject ArtilleryTowerPrefab;
    [SerializeField]
    private GameObject PowerPlantPrefab;
    [SerializeField]
    private GameObject MinePrefab;
    [SerializeField]
    private GameObject WorkshopPrefab;
    [SerializeField]
    private GameObject ShipyardPrefab;
    [SerializeField]
    private GameObject NexusPrefab;
    [SerializeField]
    private GameObject SettlementPrefab;

    #endregion

    void Start()
    {
        Debug.Assert(!Instance, "Only one BuildManager script is allowed in the scene!");

        Instance = this;


    }

    public bool TryBuild(BuildObject obj, Transform position)
    {
        PriceInfo info = GetInfo(obj);

        if (PlayerManager.Instance.GetResources().IsEnough(info.price))
        {
            Instantiate(info.prefab, position.position, position.rotation);

            PlayerManager.Instance.ChangeResource(info.price * -1);

            return true;
        }

        return false;
    }

    public PriceInfo GetInfo(BuildObject obj)
    {
        switch (obj)
        {

            case BuildObject.ArtilleryTower:
                return new PriceInfo(Setting.ARTILLERY_TOWER_RES_COST, ArtilleryTowerPrefab);

            case BuildObject.Mine:
                return new PriceInfo(Setting.MINE_RES_COST, MinePrefab);

            case BuildObject.PowerPlant:
                return new PriceInfo(Setting.POWERPLANT_REST_COST, PowerPlantPrefab);

            case BuildObject.TeslaTower:
                return new PriceInfo(Setting.TESLA_TOWER_RES_COST, TeslaTowerPrefab);

            case BuildObject.Workshop:
                return new PriceInfo(Setting.WORKSHOP_RES_COST, WorkshopPrefab);

            case BuildObject.Shipyard:
                return new PriceInfo(Setting.SHIPYARD_RES_COST, ShipyardPrefab);

            case BuildObject.Nexus:
                return new PriceInfo(Setting.NEXUS_RES_COS, NexusPrefab);

            case BuildObject.Settlement:
                return new PriceInfo(Setting.SETTLEMENT_RES_COST, SettlementPrefab);

            default:
                return new PriceInfo();

        }
    }


    public struct PriceInfo
    {
        public GameObject prefab;
        public Res price;

        public PriceInfo(Res price, GameObject prefab)
        {
            this.prefab = prefab;
            this.price = price;
        }
    }



	
}
