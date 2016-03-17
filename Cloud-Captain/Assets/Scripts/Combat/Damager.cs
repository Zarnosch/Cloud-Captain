using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Damager : MonoBehaviour
{
    public int Damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != gameObject.tag)
        {
            HealthManager health = other.GetComponent<HealthManager>();

            if (health != null)
            {
                health.ChangeHealth(-Damage);
            }
        }
    }

}
