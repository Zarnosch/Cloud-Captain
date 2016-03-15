using UnityEngine;
using System.Collections;


public abstract class ABulletBehavior : MonoBehaviour 
{
    public int damage;
    public GameObject Root;

    protected GameObject target;

    void Start()
    {
        if (Root == null)
            Root = gameObject;
    }

    public void StartBullet(GameObject target)
    {
        if (Root == null)
            Root = gameObject;

        this.target = target;
        OnSpawn();
    }

    protected abstract void OnSpawn();

}
