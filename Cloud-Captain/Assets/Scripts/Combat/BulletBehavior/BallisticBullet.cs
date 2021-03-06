using UnityEngine;
using System.Collections;
using System;


[RequireComponent(typeof(Rigidbody))]
public class BallisticBullet : ABulletBehavior {


    private Rigidbody myBody;
    private float totalTimeToTarget;
    private float curFlightTime;

    public ParticleSystem ParticleSystem;


    protected override void OnSpawn()
    {

        myBody = GetComponent<Rigidbody>();

        myBody.velocity = Vector3.zero;

        Vector3 toTarget = targetTransform.position - spawnTransform.position;
        float totalDistance = toTarget.magnitude;
        toTarget.y = 0.0f;
        float distanceXz = toTarget.magnitude;

        float distanceT = (totalDistance - minDistance) / (maxDistance - minDistance);

        totalTimeToTarget = ((1.0f - distanceT) * distanceXz * 5.0f + distanceT * Mathf.Pow(distanceXz, 1.5f)) / bulletSpeed;

        Vector3 result = CalculateBestThrowSpeed(spawnTransform.position, targetTransform.position, totalTimeToTarget);

        myBody.AddForce(result, ForceMode.VelocityChange);
        curFlightTime = 0.0f;
    }

    void Update()
    {
        curFlightTime += Time.deltaTime;

        bulletRoot.transform.LookAt(bulletRoot.transform.position + myBody.velocity);


        if(curFlightTime > totalTimeToTarget * 2.0f)
        {
            Destroy(bulletRoot);
        }

    }

    private Vector3 CalculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
    {
        // calculate vectors
        Vector3 toTarget = target - origin;
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0;

        // calculate xz and y
        float y = toTarget.y;
        float xz = toTargetXZ.magnitude;

        // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
        // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
        // so xz = v0xz * t => v0xz = xz / t
        // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
        float t = timeToTarget;
        float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
        float v0xz = xz / t;

        // create result vector for calculated starting speeds
        Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
        result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
        result.y = v0y;                                // set y to v0y (starting speed of y plane)

        return result;
    }



    public void OnImpactHandler(Transform transform)
    {
        if (transform == null)
            return;

        else
        {
            //spawn explosion:
            GameObject obj = (GameObject)Instantiate(BuildManager.Instance.ExplosionPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<SphereCollider>().radius = secondaryRange;
            obj.GetComponent<Damager>().Damage = secondaryDamage;

          
             
        }
    }

}
