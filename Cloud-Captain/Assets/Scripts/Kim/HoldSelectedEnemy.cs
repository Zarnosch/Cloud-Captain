using UnityEngine;
using System.Collections;

public class HoldSelectedEnemy : MonoBehaviour {

    public GameObject Enemy = null;
    public GameObject EnemySelectItem;
    private GameObject selectRingHolder = null;

    private int ship;
    private float shipHeight = 0.0f;

    public bool isSelected = false;
	// Use this for initialization
	void Start () {
        ship = LayerMask.NameToLayer("Ships");
        shipHeight = Setting.SHIP_FLIGHT_HEIGHT;
    }
	
	// Update is called once per frame
	void Update () {
        if(Enemy == null)
        {
            selectRingHolder = null;
        }
        if(selectRingHolder != null)
        {
            selectRingHolder.SetActive(false);
            if (isSelected)
                selectRingHolder.SetActive(true);
        }

        //GameObject newInstance = Instantiate(PlayerSelectItem, g.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity) as GameObject;
        //newInstance.transform.parent = g.transform;
    }

    public void newEnemyTarget(GameObject Enemy)
    {
        if (selectRingHolder != null)
            Destroy(selectRingHolder);
        this.Enemy = Enemy;
        if(Enemy.layer == ship)
        {
            GameObject newInstance = Instantiate(EnemySelectItem, Enemy.transform.position - new Vector3(0, shipHeight, 0), Quaternion.identity) as GameObject;
            newInstance.transform.parent = Enemy.transform;
            selectRingHolder = newInstance;
        }
        else
        {
            GameObject newInstance = Instantiate(EnemySelectItem, Enemy.transform.position, Quaternion.identity) as GameObject;
            newInstance.transform.parent = Enemy.transform;
            selectRingHolder = newInstance;
        }
        
    }

    public void clearEnemyTarget()
    {
        if (selectRingHolder != null)
            Destroy(selectRingHolder);
        if (Enemy != null)
            Enemy = null;
    }
}
