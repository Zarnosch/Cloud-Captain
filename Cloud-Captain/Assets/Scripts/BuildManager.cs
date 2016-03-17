using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour
{
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

    #endregion

    void Start()
    {
        Debug.Assert(!Instance, "Only one BuildManager script is allowed in the scene!");

        Instance = this;
    }




	
}
