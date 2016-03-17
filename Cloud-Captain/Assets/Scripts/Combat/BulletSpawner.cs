using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
    public float MinDistance = 0.0f;
    public bool RotateBulletToTarget = false;

    public VoidEvent BulletSpawned;


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
                Attack(currentTarget);

            else if (potentialTargetsInArea.Count > 0)
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

    private bool IsViableTargetObject(GameObject target)
    {
        bool canAttackLayer = true;

        if (targetType != TargetType.Both)
            canAttackLayer = target.layer == LayerMask.NameToLayer(targetType.ToString());

        return target.tag != gameObject.tag && canAttackLayer;
    }

    public void Attack(GameObject target)
    {
        if (!IsViableTargetObject(target))
        {
            return;
        }

        else 
        {
            Vector3 toTarget = target.transform.position - BulletSpawnTransform.position;
            float distance = toTarget.magnitude;


            if(distance > sphereCollider.radius || distance < MinDistance)
            {
                return;
            }

            //is the spawner ready to spawn bullets? (cooldown)
            else if (CanAttack)
            {
                curAttackCooldown = attackCooldown;

                if (spawnMode == SpawnMode.Instantiate || spawnedBullet == null)
                {
                    InstantiateBulletPrefab(target);
                }
                  

                else if (spawnMode == SpawnMode.ReUse)
                {
                    spawnedBullet.StartBullet(target, BulletSpawnTransform, MinDistance, sphereCollider.radius);
                }

                if (RotateBulletToTarget)
                {
                    spawnedBullet.transform.LookAt(target.transform);
                }

                BulletSpawned.Invoke();
            }
        }

   
    }

    void OnTriggerEnter(Collider collider)
    {
        if(IsViableTargetObject(collider.gameObject))
            potentialTargetsInArea.Add(collider.gameObject);
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
            Debug.Assert(spawnedBullet != null, "A spawned bullet needs an ABulletBehavior script!");
            spawnedBullet.StartBullet(target, BulletSpawnTransform, MinDistance, sphereCollider.radius);
            spawnedBullet.tag = gameObject.tag;

            if (AttachBulletToSpawner)
                spawnedBullet.transform.SetParent(gameObject.transform);
        }

    }


}
