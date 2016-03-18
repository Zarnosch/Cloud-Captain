using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SphereCollider))]
public class BulletSpawner : MonoBehaviour 
{
    public enum TargetType { Buildings, Ships, Both }
    public enum SpawnMode { Instantiate, ReUse }

    public TargetType targetType = TargetType.Both;
    public SpawnMode spawnMode = SpawnMode.Instantiate;
    public bool AttachBulletToSpawner = false;
    public float attackCooldown;
    public GameObject BulletPrefab;
    public Transform BulletSpawnTransform;
    public bool RotateBulletToTarget = false;
    public float MinDistance = 0.0f;
    public VoidEvent BulletSpawned;

    [ReadOnly]
    public int BulletDamage;
    [ReadOnly]
    public float BulletSpeed;
    [ReadOnly]
    public int SecondaryDamage;
    [ReadOnly]
    public float SecondaryRange;


    public float GetAttackRange()
    {
        return sphereCollider.radius;
    }

    private ABulletBehavior spawnedBullet;
    private List<GameObject> potentialTargetsInArea;
    private GameObject currentTarget;
    private float curAttackCooldown;
    private SphereCollider sphereCollider;

    public bool CanAttack { get { return curAttackCooldown <= 0.0f; } }

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        potentialTargetsInArea = new List<GameObject>();

        curAttackCooldown = attackCooldown;

        if (BulletSpawnTransform == null)
            BulletSpawnTransform = gameObject.transform;
    }

    void Update()
    {
        if (!CanAttack)
        {
            curAttackCooldown -= Time.deltaTime;
        }

        else
        {
            if(currentTarget != null)
            {
                if (!Attack(currentTarget))
                {
                    currentTarget = ChangeTarget();
                }
            }
                

            else 
            {
                while (potentialTargetsInArea.Count > 0 && potentialTargetsInArea[0] == null)
                {
                    potentialTargetsInArea.RemoveAt(0);
                }

                if (potentialTargetsInArea.Count > 0)
                {
                    currentTarget = potentialTargetsInArea[0];
                }
            }
            
        }
    }

    private GameObject ChangeTarget()
    {
        return potentialTargetsInArea[UnityEngine.Random.Range(0, potentialTargetsInArea.Count)];
    }

    private bool IsViableTargetObject(GameObject target)
    {
        bool canAttackLayer = target.layer == LayerMask.NameToLayer(targetType.ToString());

        if (targetType == TargetType.Both)
        {
            canAttackLayer = target.layer == LayerMask.NameToLayer(TargetType.Buildings.ToString()) || target.layer == LayerMask.NameToLayer(TargetType.Ships.ToString());
        }
           


        return target.tag != gameObject.tag && canAttackLayer;
    }

    public bool Attack(GameObject target)
    {

        if (!IsViableTargetObject(target))
        {
            return false;
        }

        else 
        {
            Vector3 toTarget = target.transform.position - BulletSpawnTransform.position;
            float distance = toTarget.magnitude;


            if(distance > sphereCollider.radius || distance < MinDistance)
            {
                return false;
            }

            //is the spawner ready to spawn bullets? (cooldown)
            else if (CanAttack)
            {
                curAttackCooldown = attackCooldown;

                if (spawnMode == SpawnMode.Instantiate || spawnedBullet == null)
                {
                    InstantiateBulletPrefab(target);
                    SendValuesToBullet(target);
                }
                  

                else if (spawnMode == SpawnMode.ReUse)
                {
                    SendValuesToBullet(target);
                }

                if (RotateBulletToTarget)
                {
                    spawnedBullet.transform.LookAt(target.transform);
                }

                BulletSpawned.Invoke();
            }

            return true;
        }

   
    }

    void OnTriggerEnter(Collider collider)
    {
        if (IsViableTargetObject(collider.gameObject))
        {
            potentialTargetsInArea.Add(collider.gameObject);
         
        }
 
    }

    void OnTriggerExit(Collider collider)
    {
        if (IsViableTargetObject(collider.gameObject))
            potentialTargetsInArea.Remove(collider.gameObject);
    }

    

    protected void InstantiateBulletPrefab(GameObject target)
    {
        if (BulletPrefab)
        {
            GameObject spawnedObject = (GameObject)Instantiate(BulletPrefab, BulletSpawnTransform.position, Quaternion.identity);
            spawnedBullet = spawnedObject.GetComponent<ABulletBehavior>();
            spawnedBullet.tag = gameObject.tag;
            Debug.Assert(spawnedBullet != null, "A spawned bullet needs an ABulletBehavior script!");

            if (AttachBulletToSpawner)
                spawnedBullet.transform.SetParent(gameObject.transform);
        }

    }

    private void SendValuesToBullet(GameObject target)
    {
        spawnedBullet.SetSecondaryParameter(SecondaryRange, SecondaryDamage);

        GameObject realTarget = target;

        AttackPivot pivot = target.GetComponent<AttackPivot>();

        if (pivot)
            realTarget = pivot.Pivot;

        spawnedBullet.StartBullet(realTarget.transform, BulletSpawnTransform, MinDistance, sphereCollider.radius, BulletSpeed, BulletDamage);
    }


}
