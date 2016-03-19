using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShipBuilder : MonoBehaviour
{


    public BuildManager.ShipType[] BuildableShips;
    public Transform SpawnPosition;


    [ReadOnly]
    public float curBuildCooldown;
    [ReadOnly]
    public float buildReduction = 1.0f;

    private bool isBuilding = false;
 
    public Queue<BuildManager.ShipInfo> enqueuedShips = new Queue<BuildManager.ShipInfo>();

    private Action<GameObject> queueChanged;

    private GameObject progressBarGameObject;
    private WorldSpaceBar progressBar;

    void Start()
    {
        if (SpawnPosition == null)
            SpawnPosition = gameObject.transform;


        if (!progressBarGameObject)
        {
            progressBarGameObject = (GameObject)Instantiate(BuildManager.Instance.ProgressbarPrefab);
            progressBar = progressBarGameObject.GetComponent<WorldSpaceBar>();

            progressBarGameObject.transform.position = gameObject.transform.position + new Vector3(Setting.PROGRESS_BAR_OFFSET_X, Setting.PROGRESS_BAR_OFFSET_Y, Setting.PROGRESS_BAR_OFFSET_Z);

            progressBarGameObject.transform.SetParent(gameObject.transform);

            TryActivateBar();
  
        }
    }

    private void TryActivateBar()
    {
        if (enqueuedShips.Count == 0)
        {
            if(progressBarGameObject.activeSelf)
                progressBarGameObject.SetActive(false);
        }

        else
        {
            if(!progressBarGameObject.activeSelf)
                progressBarGameObject.SetActive(true);

        }

    }


    public void BuildShip(BuildManager.ShipType type)
    {
        InternBuildShip(type, true);
    }

    public void BuildShipNoCost(BuildManager.ShipType type)
    {
        InternBuildShip(type, false);
    }

    private void InternBuildShip(BuildManager.ShipType type, bool cost)
    {
        if (CanBuild(type))
        {
            BuildManager.ShipInfo info = BuildManager.Instance.GetShipInfo(type);

            Res price = new Res(0, 0, 0);

            if (cost)
                price = info.price;

            if (PlayerManager.Instance.GetResources().IsEnough(price) || BuildManager.Instance.NoCostMode)
            {
                if(!BuildManager.Instance.NoCostMode)
                    PlayerManager.Instance.ChangeResource(price * -1);

                enqueuedShips.Enqueue(info);
                QueueChanged();
            }
        }
    }

    public bool CanBuild(BuildManager.ShipType type)
    {
        for (int i = 0; i < BuildableShips.Length; i++)
        {
            if (BuildableShips[i] == type)
                return true;
        }

        return false;
    }


    void Update()
    {

        if (isBuilding)
        {
            curBuildCooldown -= Time.deltaTime;

            progressBar.SetPercent(GetCurrentBuildPercent());

            if(curBuildCooldown <= 0.0f || BuildManager.Instance.InstantBuild)
            {
                GameObject newShip = (GameObject) Instantiate(enqueuedShips.Dequeue().prefab, SpawnPosition.transform.position, Quaternion.identity);
               
                QueueChanged();
                isBuilding = false;
            }
        }

        else if(enqueuedShips.Count > 0)
        {
            isBuilding = true;

            float reduction = 1.0f - buildReduction;
            curBuildCooldown = enqueuedShips.Peek().buildTime - (enqueuedShips.Peek().buildTime * reduction);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            BuildShipNoCost(BuildManager.ShipType.Scout);
        }

    }


    private void QueueChanged()
    {
        if (queueChanged != null)
        {
            queueChanged(this.gameObject);
        }

        TryActivateBar();
            
    }

    public void AddQueueChangedListener(Action<GameObject> action)
    {
        queueChanged += action;
    }

    public void RemoveQueueChangedListener(Action<GameObject> action)
    {
        queueChanged -= action;
    }

    public float GetCurrentBuildPercent()
    {

        if(enqueuedShips.Count > 0)
        {
            return curBuildCooldown / enqueuedShips.Peek().buildTime;
        }

        else
            return 0.0f;

    }



}
