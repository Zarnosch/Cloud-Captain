using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public List<GameObject> selectedUnits = new List<GameObject>();

    [ReadOnly]
    [SerializeField]
    private List<GameObjectType> ownedUnits = new List<GameObjectType>();
    [ReadOnly]
    [SerializeField]
    private List<GameObjectType> ownedBuildings = new List<GameObjectType>();
    [ReadOnly]
    [SerializeField]
    private List<MachineProducer> ownedWorkshops = new List<MachineProducer>();
    [ReadOnly]
    [SerializeField]
    private List<GameObject> controlledIslands = new List<GameObject>();

    [ReadOnly]
    [SerializeField]
    private Res resources = new Res(0, 0, 0);
    public Res GetCurrentResources() { return resources; }

    [ReadOnly]
    [SerializeField]
    private int currentSupply;
    public int GetCurrentSupply() { return currentSupply; }

    [ReadOnly]
    [SerializeField]
    private int supplyLimit;
    public int GetSupplyLimi() { return supplyLimit; }


    public UIManager UIManager;

    // Use this for initialization
    void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Multiple PlayerManagers detected, removing: " + this.gameObject.name);
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        resources.Energy = Setting.START_ENERGY_AMOUNT;
        resources.Matter = Setting.START_MATTER_AMOUNT;
        resources.Engine = Setting.START_ENGINE_AMOUNT;
        supplyLimit = Setting.SUPPLY_MAX_START;
    }

    public void ChangeResource(Res res)
    {
        ChangeResource(res.Matter, res.Energy, res.Engine);
    }

    public void ChangeResource(int matter, int energy, int engine)
    {
        if (BuildManager.Instance.NoCostMode)
        {
            if (matter < 0)
                matter = 0;

            if (energy < 0)
                energy = 0;

            if (engine < 0)
                engine = 0;
        }

        resources.Matter += matter;
        resources.Matter = Mathf.Clamp(resources.Matter, 0, Setting.MAX_RES);

        resources.Energy += energy;
        resources.Energy = Mathf.Clamp(resources.Energy, 0, Setting.MAX_RES);

        resources.Engine += engine;
        resources.Engine = Mathf.Clamp(resources.Engine, 0, Setting.MAX_RES);
    }

    public void ChangeSupply(int supplyCost)
    {
        this.currentSupply += supplyCost;

        //using asserts instead of clamp here, since it should be an error to have more or less supply than the limit
        Debug.Assert(this.currentSupply <= supplyLimit);
        Debug.Assert(this.currentSupply >= 0);
    }

    public bool EnoughSupply(int supplyCost)
    {
        return this.currentSupply + supplyCost <= this.supplyLimit;
    }

    private void AddOwnedShip(GameObjectType type)
    {
        ownedUnits.Add(type);
    }

    private void AddOwnedBuilding(GameObjectType type)
    {
        if (type.ObjectType == Setting.ObjectType.Workshop)
        {
            MachineProducer producer = type.gameObject.GetComponent<MachineProducer>();

            if (producer)
                ownedWorkshops.Add(producer);
        }

        if (type.ObjectType == Setting.ObjectType.Nexus)
        {
            IslandReference island = type.gameObject.GetComponent<IslandReference>();
            controlledIslands.Add(island.island.gameObject);

            if (controlledIslands.Count >= 2)
                this.supplyLimit += Setting.SUPPLY_PLUS_PER_NEXUS;
        }

        ownedBuildings.Add(type);
    }

    public void AddUnit(GameObjectType gameobjectType, bool paidSupply)
    {
        if (!paidSupply)
            ChangeSupply(BuildManager.Instance.GetUnitInfo(gameobjectType.ObjectType).SupplyCost);

        if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Ships"))
        {
            AddOwnedShip(gameobjectType);
        }

        else if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            AddOwnedBuilding(gameobjectType);
        }
    }

    public void RemoveUnit(GameObjectType gameobjectType)
    {
        this.ChangeSupply(-BuildManager.Instance.GetUnitInfo(gameobjectType.ObjectType).SupplyCost);

        if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Ships"))
        {
           RemoveOwnedShip(gameobjectType);
        }

        else if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
           RemoveOwnedBuilding(gameobjectType);
        }
    }

    private void RemoveOwnedBuilding(GameObjectType type)
    {
        if (type.ObjectType == Setting.ObjectType.Workshop)
        {
            MachineProducer producer = type.gameObject.GetComponent<MachineProducer>();

            if (producer)
                ownedWorkshops.Remove(producer);
        }

        if (type.ObjectType == Setting.ObjectType.Nexus)
        {
            IslandReference island = type.gameObject.GetComponent<IslandReference>();
            controlledIslands.Remove(island.island.gameObject);

            if(controlledIslands.Count >= 1)
                this.supplyLimit -= Setting.SUPPLY_PLUS_PER_NEXUS;
        }

        ownedBuildings.Remove(type);
    }

    private void RemoveOwnedShip(GameObjectType type)
    {
        ownedUnits.Remove(type);
    }

    public bool TryProduceMachine()
    {
        return TryProduceMachineIntern(Setting.COST_RES_ENGINE);
    }

    public void ProduceMachineNoCost(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            TryProduceMachineIntern(Res.Zero);
        }
    }

    public bool HasWorkshops()
    {
        return ownedWorkshops.Count > 0;
    }

    private bool TryProduceMachineIntern(Res cost)
    {
        if (this.ownedWorkshops.Count == 0)
            return false;

        else
        {
            if (PlayerManager.Instance.EnoughResource(Setting.COST_RES_ENGINE))
            {
                if(ownedWorkshops.Count  > 0)
                {
                    MachineProducer lowProducer = ownedWorkshops[0];

                    for (int i = 1; i < ownedWorkshops.Count; i++)
                    {
                        if (ownedWorkshops[i].NumMachinesInQueue < lowProducer.NumMachinesInQueue)
                        {
                            lowProducer = ownedWorkshops[i];
                        }
                    }

                    lowProducer.ProduceMachine();

                    if(!BuildManager.Instance.NoCostMode)
                        ChangeResource(Setting.COST_RES_ENGINE * -1);

                    return true;
                }
                             
            }
        }

        return false;

    }

    public bool EnoughResource(Res res)
    {
        return EnoughResource(res.Matter, res.Energy, res.Engine);
    }

    public bool EnoughResource(int matter, int energy, int engine)
    {
        if (BuildManager.Instance.NoCostMode)
            return true;
        else
            return resources.Matter >= matter && resources.Energy >= energy && resources.Engine >= engine;
    }
}
