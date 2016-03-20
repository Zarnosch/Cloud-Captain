using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class LinearMover : ABulletBehavior 
{

    private Vector3 startPos;
    
    private Rigidbody myBody;

    protected override void OnSpawn()
    {
        myBody = GetComponent<Rigidbody>();

        myBody.velocity = Vector3.zero;

        Vector3 offset = Random.insideUnitSphere * Setting.SHIP_BULLET_OFFSET_HORIZONTAL;
        offset.y = (Random.value * -0.5f) * Setting.SHIP_BULLET_OFFSET_VERTICAL;

        Vector3 targetPos = targetTransform.position;

        targetPos += offset;

        Vector3 direction = targetPos - transform.position;
        direction.Normalize();

        myBody.AddForce(direction * bulletSpeed, ForceMode.VelocityChange);

        startPos = bulletRoot.transform.position;
    }

    void Update()
    {
        float distance = (bulletRoot.transform.position - startPos).magnitude;

        if (distance > maxDistance * 1.1f)
            bulletSpawner.DestroyBullet(this);
    }

}
