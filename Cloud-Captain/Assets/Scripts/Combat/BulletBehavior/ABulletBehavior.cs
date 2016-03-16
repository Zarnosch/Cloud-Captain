using UnityEngine;
using System.Collections;


public abstract class ABulletBehavior : MonoBehaviour 
{
    public int Damage = 0;
    public bool KillOnImpact = false;

    public TransformEvent OnImpact;

    protected GameObject bulletRoot;
    protected Transform spawnTransform;

    protected float minDistance;
    protected float maxDistance;
    protected GameObject target;


    public void StartBullet(GameObject target, Transform spawnPoint, float minDist, float maxDist)
    {            
        if (bulletRoot == null)
            bulletRoot = gameObject;

        this.spawnTransform = spawnPoint;
        this.target = target;
        this.minDistance = minDist;
        this.maxDistance = maxDist;

        OnSpawn();
    }

    protected abstract void OnSpawn();


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            HealthManager health = other.GetComponent<HealthManager>();

            if (health != null)
            {
                health.ChangeHealth(-Damage);
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
