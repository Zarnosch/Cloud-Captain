using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FollowMissile : ABulletBehavior {

    public float speed = 5.0f;
    [Range(0.0f, 1.0f)]
    public float percentChange = 0.5f;

    private float flightTime;
    private float maxFlightTime = 2.0f;

    Rigidbody myBody;

    Vector3 oldDirection;
    Vector3 curDirection;

    protected override void OnSpawn()
    {
        myBody = GetComponent<Rigidbody>();
        flightTime = 0.0f;


        Vector3 toTarget = target.transform.position - Root.transform.position;
        toTarget.Normalize();

        myBody.AddForce(toTarget * speed, ForceMode.VelocityChange);
    }

    void Update()
    {
        if (target == null)
            return;

        else
        {
            flightTime += Time.deltaTime;
            float t = flightTime / maxFlightTime;
            t = Mathf.Clamp(t, 0.0f, 1.0f);

            Vector3 toTarget = target.transform.position - Root.transform.position;
            float distance = toTarget.magnitude;
            toTarget.Normalize();

            oldDirection = curDirection;
            curDirection = toTarget;



            myBody.velocity = Vector3.Lerp(oldDirection, curDirection, t) * speed * t * distance;
          
        }
    }
}
