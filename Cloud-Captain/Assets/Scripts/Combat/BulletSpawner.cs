using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SphereCollider))]
public class BulletSpawner : MonoBehaviour 
{
    //prefab gameobject to instantiates bullets:
    private static Dictionary<GameObject, Queue<ABulletBehavior>> bulletPool = new Dictionary<GameObject, Queue<ABulletBehavior>>();


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
        if (potentialTargetsInArea.Count == 0)
            return null;
        else
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
            spawnedBullet = TryGetFromPool(BulletPrefab);


            if (spawnedBullet == null)
            {
                GameObject tmpGameObject = (GameObject)Instantiate(BulletPrefab);
                spawnedBullet = tmpGameObject.GetComponent<ABulletBehavior>();
                spawnedBullet.Prefab = BulletPrefab;
            }

            spawnedBullet.bulletRoot.SetActive(true);

#if UNITY_EDITOR
            spawnedBullet.bulletRoot.hideFlags = HideFlags.None;
#endif

            Debug.Assert(spawnedBullet != null, "A spawned bullet needs an ABulletBehavior script!");

            if (AttachBulletToSpawner)
                spawnedBullet.transform.SetParent(gameObject.transform);
        }

    }

    private ABulletBehavior TryGetFromPool(GameObject prefab)
    {
        ABulletBehavior tmpBulletBehave = null;

        Queue<ABulletBehavior> pool = null;

        if (bulletPool.TryGetValue(prefab, out pool))
        {
            if (pool.Count > 0)
            {
                tmpBulletBehave = pool.Dequeue();
            }

        }

        else
        {
            bulletPool.Add(BulletPrefab, new Queue<ABulletBehavior>());
        }

        return tmpBulletBehave;
    }

    private void SendValuesToBullet(GameObject target)
    {
        GameObject realTarget = target;

        AttackPivot pivot = target.GetComponent<AttackPivot>();

        if (pivot)
            realTarget = pivot.Pivot;

        spawnedBullet.bulletRoot.transform.position = BulletSpawnTransform.position;
        spawnedBullet.bulletRoot.tag = gameObject.tag;

        if (RotateBulletToTarget)
            spawnedBullet.transform.LookAt(target.transform);

        spawnedBullet.StartBullet(this, realTarget.transform, BulletSpawnTransform, MinDistance, sphereCollider.radius, BulletSpeed, BulletDamage, SecondaryRange, SecondaryDamage);
    }

    public void DestroyBullet(ABulletBehavior bullet)
    {
        bullet.bulletRoot.SetActive(false);

#if UNITY_EDITOR
        bullet.bulletRoot.hideFlags = HideFlags.HideInHierarchy;
#endif

        bulletPool[bullet.Prefab].Enqueue(bullet);
    }

}
