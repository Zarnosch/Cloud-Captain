using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public List<GameObject> selectedUnits = new List<GameObject>();


    [Header("Owned buildings and units")]
    [ReadOnly]
    [SerializeField]
    private List<GameobjectType> ownedUnits = new List<GameobjectType>();
    [ReadOnly]
    [SerializeField]
    private List<GameobjectType> ownedBuildings = new List<GameobjectType>();
    [ReadOnly]
    [SerializeField]
    private List<MachineProducer> ownedWorkshops = new List<MachineProducer>();
    [ReadOnly]
    [SerializeField]
    private List<RessourceProducer> ownedResourceProducer = new List<RessourceProducer>();
    [ReadOnly]
    [SerializeField]
    private List<GameObject> controlledIslands = new List<GameObject>();


    [Header("Resources and supply")]
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

    [ReadOnly]
    [SerializeField]
    private float energyPerSecond;
    public float GetEnergyPerSecond() { return energyPerSecond; }

    [ReadOnly]
    [SerializeField]
    private float matterPerSecond;
    public float GetMatterPerSecond() { return matterPerSecond; }


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

    private void AddOwnedShip(GameobjectType type)
    {
        ownedUnits.Add(type);
    }

    private void AddOwnedBuilding(GameobjectType type)
    {
        if (type.ObjectType == Setting.ObjectType.Workshop)
        {
            MachineProducer producer = type.gameObject.GetComponent<MachineProducer>();

            if (producer)
                ownedWorkshops.Add(producer);
        }

        else if (type.ObjectType == Setting.ObjectType.Nexus)
        {
            IslandReference island = type.gameObject.GetComponent<IslandReference>();
            controlledIslands.Add(island.island.gameObject);

            if (controlledIslands.Count >= 2)
                this.supplyLimit += Setting.SUPPLY_PLUS_PER_NEXUS;

            CheckForRessourceProducer(type.gameObject, false);
        }
        else if (type.ObjectType == Setting.ObjectType.Mine || type.ObjectType == Setting.ObjectType.PowerPlant)
        {
            CheckForRessourceProducer(type.gameObject, false);
        }

        ownedBuildings.Add(type);
    }

    private void CalculateResourcesPerSecond()
    {
        this.energyPerSecond = 0.0f;
        this.matterPerSecond = 0.0f;

        for (int i = 0; i < ownedResourceProducer.Count; i++)
        {
            this.energyPerSecond += ownedResourceProducer[i].GetEnergyPerSecond();
            this.matterPerSecond += ownedResourceProducer[i].GetMatterPerSecond();
        }
    }

    private void CheckForRessourceProducer(GameObject gameObject, bool remove)
    {
        RessourceProducer resourceProducer = gameObject.GetComponent<RessourceProducer>();

        if (resourceProducer)
        {
            if(remove)
                this.ownedResourceProducer.Remove(resourceProducer);
            else
                this.ownedResourceProducer.Add(resourceProducer);

            CalculateResourcesPerSecond();
        }
    }

    public void AddUnit(GameobjectType gameobjectType, bool paidSupply)
    {
        if (!paidSupply)
            ChangeSupply(BuildManager.Instance.GetUnitBuildInfo(gameobjectType.ObjectType).SupplyCost);

        if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Ships"))
        {
            AddOwnedShip(gameobjectType);
        }

        else if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            AddOwnedBuilding(gameobjectType);
        }
    }

    public void RemoveUnit(GameobjectType gameobjectType)
    {
        this.ChangeSupply(-BuildManager.Instance.GetUnitBuildInfo(gameobjectType.ObjectType).SupplyCost);

        if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Ships"))
        {
           RemoveOwnedShip(gameobjectType);
        }

        else if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
           RemoveOwnedBuilding(gameobjectType);
        }
    }

    private void RemoveOwnedBuilding(GameobjectType type)
    {
        if (type.ObjectType == Setting.ObjectType.Workshop)
        {
            MachineProducer producer = type.gameObject.GetComponent<MachineProducer>();

            if (producer)
                ownedWorkshops.Remove(producer);
        }

        else if (type.ObjectType == Setting.ObjectType.Nexus)
        {
            IslandReference island = type.gameObject.GetComponent<IslandReference>();
            controlledIslands.Remove(island.island.gameObject);

            if(controlledIslands.Count >= 1)
                this.supplyLimit -= Setting.SUPPLY_PLUS_PER_NEXUS;

            CheckForRessourceProducer(type.gameObject, true);
        }

        else if (type.ObjectType == Setting.ObjectType.Mine || type.ObjectType == Setting.ObjectType.PowerPlant)
        {
            CheckForRessourceProducer(type.gameObject, true);
        }

        ownedBuildings.Remove(type);
    }

    private void RemoveOwnedShip(GameobjectType type)
    {
        ownedUnits.Remove(type);
    }

    public ProduceMachineFeedback TryProduceMachine()
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

    private ProduceMachineFeedback TryProduceMachineIntern(Res cost)
    {
        if (this.ownedWorkshops.Count == 0)
        {
            return ProduceMachineFeedback.NoWorkshops;
        }

        else
        {
            if (EnoughResource(Setting.COST_RES_ENGINE))
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

                    return ProduceMachineFeedback.Success;
                }                       
            }
            
        }

        return ProduceMachineFeedback.NotEnoughRessources;

    }

    public bool EnoughResource(Res res)
    {
        return EnoughResource(res.Matter, res.Energy, res.Engine);
    }

    public Res GetMissingResources(Res cost)
    {
        Res delta = this.resources - cost;

        //if cost was bigger than current ressources, then use the difference as missing resources:
        //otherwise this ressource is not needed anymore -> set it to 0

        if (delta.Energy < 0)
            delta.Energy = delta.Energy * -1;
        else
            delta.Energy = 0;

        if (delta.Matter < 0)
            delta.Matter = delta.Matter * -1;
        else
            delta.Matter = 0;

        if (delta.Engine < 0)
            delta.Engine = delta.Engine * -1;
        else
            delta.Engine = 0;

        return delta;
    }

    public bool EnoughResource(int matter, int energy, int engine)
    {
        if (BuildManager.Instance.NoCostMode)
            return true;
        else
            return resources.Matter >= matter && resources.Energy >= energy && resources.Engine >= engine;
    }
}
