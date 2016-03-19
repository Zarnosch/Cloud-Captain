using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public List<GameObject> selectedUnits = new List<GameObject>();


    private List<GameobjectType> ownedUnits = new List<GameobjectType>();
    private List<GameobjectType> ownedBuildings = new List<GameobjectType>();
    private List<MachineProducer> workshops = new List<MachineProducer>();
    private List<GameobjectType> islands = new List<GameobjectType>();

    [ReadOnly]
    public Res resources = new Res(0, 0, 0);

	public UIManager UIManager;

    // Use this for initialization
    void Awake()
    {
        Debug.Assert(!Instance, "Only one PlayerManager script is allowed in one scene!");

        Instance = this;

        resources.Energy = Setting.START_ENERGY_AMOUNT;
        resources.Matter = Setting.START_MATTER_AMOUNT;
        resources.Engine = Setting.START_ENGINE_AMOUNT;
    }

    public void ChangeResource(Res res)
    {
        ChangeResource(res.Matter, res.Energy, res.Engine);
    }

    public void ChangeResource(int matter, int energy, int engine)
    {
        resources.Matter += matter;
        resources.Matter = Mathf.Clamp(resources.Matter, 0, Setting.MAX_RES);

        resources.Energy += energy;
        resources.Energy = Mathf.Clamp(resources.Energy, 0, Setting.MAX_RES);

        resources.Engine += engine;
        resources.Engine = Mathf.Clamp(resources.Engine, 0, Setting.MAX_RES);
    }

    public Res GetResources()
    {
        return resources;
    }

    public void AddOwnedShip(GameobjectType type)
    {
        ownedUnits.Add(type);
    }

    public void AddOwnedBuilding(GameobjectType type)
    {
        if (type.ObjectType == Setting.ObjectType.Workshop)
        {
            MachineProducer producer = type.gameObject.GetComponent<MachineProducer>();

            if (producer)
                workshops.Add(producer);
        }

        ownedBuildings.Add(type);
    }


    public void AddBuiltObject(GameobjectType gameobjectType)
    {
        if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Ships"))
        {
            AddOwnedShip(gameobjectType);
        }

        else if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            AddOwnedBuilding(gameobjectType);
        }

        else if (gameobjectType.gameObject.layer == LayerMask.NameToLayer("Islands"))
        {
            AddOwnedIsland(gameobjectType);
        }
    }

    private void AddOwnedIsland(GameobjectType gameobjectType)
    {
        islands.Add(gameobjectType);
    }

    public bool TryProduceMachine()
    {
        return TryProduceMachineIntern(Setting.COST_RES_ENGINE);
    }

    public bool TryProduceMachineNoCost()
    {
        return TryProduceMachineIntern(Res.Zero);
    }

    public bool HasWorkshops()
    {
        return workshops.Count > 0;
    }

    private bool TryProduceMachineIntern(Res cost)
    {
        if (this.workshops.Count == 0)
            return false;

        else
        {
            if (resources.IsEnough(Setting.COST_RES_ENGINE))
            {
                if(workshops.Count  > 0)
                {
                    MachineProducer lowProducer = workshops[0];

                    for (int i = 1; i < workshops.Count; i++)
                    {
                        if (workshops[i].NumMachinesInQueue < lowProducer.NumMachinesInQueue)
                        {
                            lowProducer = workshops[i];
                        }
                    }

                    lowProducer.ProduceMachine();
                    ChangeResource(Setting.COST_RES_ENGINE * -1);
                    return true;
                }
                             
            }
        }

        return false;

    }
}
