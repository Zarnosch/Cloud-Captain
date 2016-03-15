using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class BeamBullet : ABulletBehavior 
{
    public float visibleTime = 0.25f;
    private float currentVisibleTime;

    private LineRenderer lineRenderer;

    protected override void OnSpawn()
    {
        currentVisibleTime = 0.0f;
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, Root.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);

        Vector3 toTarget = target.transform.position - Root.transform.position;
        float distance = toTarget.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(Root.transform.position, toTarget, distance);

        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log("Hit: " + hits[i].collider.gameObject.name);
        }

    }

    void Update()
    {
        currentVisibleTime += Time.deltaTime;

        if (currentVisibleTime >= visibleTime)
        {
            Destroy(Root);
        }
    }
}
