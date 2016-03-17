using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
    public static PlayerManager Instance { get; private set; }

    public List<GameObject> selectedUnits;


    [ReadOnly]
    public Res resources = new Res(0, 0, 0);

	// Use this for initialization
	void Start () 
    {
        Debug.Assert(!Instance, "Only one PlayerManager script is allowed in one scene!");
       
        Instance = this;
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
	

}
