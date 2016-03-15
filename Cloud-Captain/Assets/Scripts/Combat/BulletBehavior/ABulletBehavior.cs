using UnityEngine;
using System.Collections;


public abstract class ABulletBehavior : MonoBehaviour 
{
    public int damage;

    protected GameObject Target;

    public void StartBullet(GameObject target)
    {
        this.Target = target;
        OnSpawn();
    }

    protected abstract void OnSpawn();

}
