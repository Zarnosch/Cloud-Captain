using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Damager : MonoBehaviour 
{
    public int Damage;

    public GameObject RootObject;

    void Start()
    {
        if (RootObject == null)
            RootObject = gameObject;
    }


    void OnTriggerEnter(Collider other)
    {
        HealthManager health = other.GetComponent<HealthManager>();

        if (health != null)
        {
            health.ChangeHealth(-Damage);
            Kill();
        }
    }


    void Kill()
    {
        Destroy(RootObject);
    }
}
