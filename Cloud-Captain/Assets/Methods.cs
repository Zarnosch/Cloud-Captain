using UnityEngine;
using System.Collections;

public class Methods : MonoBehaviour {

    PlayerManager playerInstance;
    void Start () {
        playerInstance = PlayerManager.Instance;
    }


    public void SelectedListAdd(GameObject g)
    {
        playerInstance.selectedUnits.Add(g);
    }

    public void SelectedListClear()
    {
        playerInstance.selectedUnits.Clear();
    }


}
