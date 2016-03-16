using UnityEngine;
using System.Collections;
using System;


[RequireComponent(typeof(Rigidbody))]
public class BallisticBullet : ABulletBehavior {

    public float Speed = 25.0f;
    public GameObject ExplosionPrefab;

    private Rigidbody myBody;
    private float totalTimeToTarget;
    private float curFlightTime;

    protected override void OnSpawn()
    {
        myBody = GetComponent<Rigidbody>();

        Vector3 toTarget = target.transform.position - spawnTransform.position;
        float totalDistance = toTarget.magnitude;
        toTarget.y = 0.0f;
        float distanceXz = toTarget.magnitude;

        float distanceT = (totalDistance - minDistance) / (maxDistance - minDistance);

        totalTimeToTarget = ((1.0f - distanceT) * distanceXz * 5.0f + distanceT * Mathf.Pow(distanceXz, 1.5f)) / Speed;

        Vector3 result = calculateBestThrowSpeed(spawnTransform.position, target.transform.position, totalTimeToTarget);

        myBody.AddForce(result, ForceMode.VelocityChange);
    }

    void Update()
    {
        curFlightTime += Time.deltaTime;

        if(curFlightTime > totalTimeToTarget * 2.0f)
        {
            Destroy(bulletRoot);
        }

    }

    private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
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
            Instantiate(ExplosionPrefab.gameObject, transform.position, Quaternion.identity);
        }
    }

}
