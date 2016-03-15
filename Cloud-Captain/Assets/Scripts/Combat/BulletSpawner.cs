using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(SphereCollider))]
public class BulletSpawner : MonoBehaviour 
{
    public float attackCooldown;
    private float curAttackCooldown;

    public bool CanAttack { get { return curAttackCooldown <= 0.0f; } }

    public GameObject BulletPrefab;
    public Transform BulletSpawnTransform;

    private GameObject target;

    void Start()
    {
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
            if (target != null)
                Attack(target);
        }
    }

    public void Attack(GameObject target)
    {


        if (CanAttack && BulletPrefab)
        {
            curAttackCooldown = attackCooldown;

            GameObject spawnedBullet = (GameObject)Instantiate(BulletPrefab, BulletSpawnTransform.position, Quaternion.identity);
            ABulletBehavior bulletBehavior = spawnedBullet.GetComponent<ABulletBehavior>();
            Debug.Assert(bulletBehavior != null, "A spawned bullet needs an ABulletBehavior script!");
            bulletBehavior.StartBullet(target);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        target = collider.gameObject;
    }


    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == target)
            target = null;
    }


}
