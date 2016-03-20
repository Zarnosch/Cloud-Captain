using UnityEngine;
using System.Collections;


public abstract class ABulletBehavior : MonoBehaviour
{

    [HideInInspector]
    public GameObject Prefab;

    public bool KillOnImpact = false;

    public TransformEvent OnImpact;

    [ReadOnly]
    public int damage = 0;
    [ReadOnly]
    public float bulletSpeed;
    [ReadOnly]
    public float secondaryRange;
    [ReadOnly]
    public int secondaryDamage;

    public GameObject bulletRoot;
    protected Transform spawnTransform;

    protected float minDistance;
    protected float maxDistance;
    protected Transform targetTransform;
    protected BulletSpawner bulletSpawner;


    public void StartBullet(BulletSpawner bulletSpawner, Transform targetTransform, Transform spawnPoint, float minDist, float maxDist, float speed, int bulletDamage, float secondaryRange, int secondaryDamage)
    {
        this.bulletSpawner = bulletSpawner;

        if (bulletRoot == null)
            bulletRoot = gameObject;

        this.spawnTransform = spawnPoint;
        this.targetTransform = targetTransform;
        this.minDistance = minDist;
        this.maxDistance = maxDist;
        this.bulletSpeed = speed;
        this.damage = bulletDamage;
        this.secondaryRange = secondaryRange;
        this.secondaryDamage = secondaryDamage;

        OnSpawn();
    }

    protected abstract void OnSpawn();


    void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.name + ", " + LayerMask.LayerToName(other.gameObject.layer) + ", " + other.tag);

        //AKA: islands
        if (other.tag == "Neutral")
        {
            OnImpact.Invoke(bulletRoot.transform);

            if (KillOnImpact)
            {
                this.bulletSpawner.DestroyBullet(this);
            }
        }

        else if (other.tag != bulletRoot.tag)
        {
            HealthManager health = other.GetComponent<HealthManager>();

            if (health != null)
            {
                health.ChangeHealth(-damage);
                OnImpact.Invoke(bulletRoot.transform);

                if (KillOnImpact)
                {
                    this.bulletSpawner.DestroyBullet(this);
                }


            }
        }

      

    }

}
