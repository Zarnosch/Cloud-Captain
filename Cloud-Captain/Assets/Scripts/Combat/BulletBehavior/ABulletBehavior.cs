using UnityEngine;
using System.Collections;


public abstract class ABulletBehavior : MonoBehaviour 
{

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

    protected GameObject bulletRoot;
    protected Transform spawnTransform;

    protected float minDistance;
    protected float maxDistance;
    protected Transform targetTransform;


    public void SetSecondaryParameter(float range, int damage)
    {
        this.secondaryRange = range;
        this.secondaryDamage = damage;
    }


    public void StartBullet(Transform targetTransform, Transform spawnPoint, float minDist, float maxDist, float speed, int bulletDamage)
    {            
        if (bulletRoot == null)
            bulletRoot = gameObject;

        this.spawnTransform = spawnPoint;
        this.targetTransform = targetTransform;
        this.minDistance = minDist;
        this.maxDistance = maxDist;
        this.bulletSpeed = speed;
        this.damage = bulletDamage;

        OnSpawn();
    }

    protected abstract void OnSpawn();


    void OnTriggerEnter(Collider other)
    {
        if (other.tag != bulletRoot.tag)
        {
            HealthManager health = other.GetComponent<HealthManager>();

            if (health != null)
            {
                health.ChangeHealth(-damage);
                OnImpact.Invoke(bulletRoot.transform);

                if (KillOnImpact)
                {
                    Destroy(bulletRoot);
                }

      
            }
        }

        //AKA: islands
        else if(other.tag == "Neutral")
        {
            OnImpact.Invoke(bulletRoot.transform);

            if (KillOnImpact)
            {
                Destroy(bulletRoot);
            }
        }
    }



}
