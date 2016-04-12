using UnityEngine;
using System.Collections;


public enum ProduceMachineFeedback { Success, NoWorkshops, NotEnoughRessources }

public enum BuildShipFeedback { Success, NotEnoughSupply, NotEnoughRessources, Unknown }

//public enum BuildBuildingFeedback { Success, NotEnoughRessources }


public struct BuildBuildingFeedback
{
    public GameObject BuiltObject { get; private set; }

    private Res MissingResources { get; set; }

    public bool WasSuccessfull() { return BuiltObject != null; }
    public bool NeedsResources() { return MissingResources.Energy != 0 || MissingResources.Engine != 0 || MissingResources.Matter != 0; }

    public bool NeedsEnergy() { return MissingResources.Energy != 0; }
    public bool NeedsMatter() { return MissingResources.Matter != 0; }
    public bool NeedsEngines() { return MissingResources.Engine != 0; }

    public BuildBuildingFeedback(GameObject building, Res neededRes)
    {
        this.BuiltObject = building;
        this.MissingResources = neededRes;
    }
}
