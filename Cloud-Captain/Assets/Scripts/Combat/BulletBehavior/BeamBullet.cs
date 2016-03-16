using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CapsuleCollider))]
public class BeamBullet : ABulletBehavior 
{
    public float LineWidth = 0.33f;

    [Range(0.0f, 1.0f)]
    public float CapsulePercentLineWidth = 1.0f;


    private LineRenderer lineRenderer;
    private CapsuleCollider myCollider;

    private bool reEnableCollider = true;

    protected override void OnSpawn()
    {

        gameObject.SetActive(true);

        if(lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        if (myCollider == null)
            myCollider = GetComponent<CapsuleCollider>();

        myCollider.radius = (LineWidth / 2.0f) * CapsulePercentLineWidth;
        myCollider.center = Vector3.zero;
        myCollider.direction = 2;

        lineRenderer.SetWidth(LineWidth, LineWidth);

        lineRenderer.SetPosition(0, spawnTransform.position);
        lineRenderer.SetPosition(1, target.transform.position);
        UpdateCapsule();

        reEnableCollider = true;

    }


    void Update()
    {
        if (target)
        {
            lineRenderer.SetPosition(0, spawnTransform.position);
            lineRenderer.SetPosition(1, target.transform.position);

            if (myCollider.enabled)
                UpdateCapsule();

            myCollider.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (target && reEnableCollider)
        {

            myCollider.enabled = true;
            reEnableCollider = false;

        }
          
    }

    void UpdateCapsule()
    {
        myCollider.transform.position = spawnTransform.position + (target.transform.position - spawnTransform.position) / 2;
        myCollider.transform.LookAt(spawnTransform.position);
        myCollider.height = (spawnTransform.position - target.transform.position).magnitude;
    }
}
