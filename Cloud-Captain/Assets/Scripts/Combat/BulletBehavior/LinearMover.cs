using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class LinearMover : ABulletBehavior 
{
    public float lifeTime = 5.0f;
    public float Speed = 1.0f;
    private Rigidbody myBody;

    protected override void OnSpawn()
    {
        myBody = GetComponent<Rigidbody>();

        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();

        myBody.AddForce(direction * Speed, ForceMode.VelocityChange);
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0.0f)
            Destroy(gameObject);
    }

}
