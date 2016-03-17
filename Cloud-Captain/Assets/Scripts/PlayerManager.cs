using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
    public static PlayerManager Instance { get; private set; }

    public List<GameObject> selectedUnits;

    //TODO: start resources:
    [SerializeField]
    private Res resources = new Res(0, 0, 0);

	// Use this for initialization
	void Start () 
    {
        Debug.Assert(!Instance, "Only one PlayerManager script is allowed in one scene!");
       
        Instance = this;
	}

    public void ChangeResource(Res res)
    {
      
        resources += res;
    }

    public void ChangeResource(int matter, int energy, int engine)
    {
        resources.Matter += matter;
        resources.Energy += energy;
        resources.Engine += engine;
    }

    public Res GetResources()
    {
        return resources;
    }
	

}
