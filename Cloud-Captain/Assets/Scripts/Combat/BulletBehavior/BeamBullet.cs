using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CapsuleCollider))]
public class BeamBullet : ABulletBehavior 
{
    public float MinLineWidth = 0.33f;
    public float MaxLineWidth = 1.0f;
    public float LineWidthChangeSpeed = 10.0f;

    private float lineWidth = 0.33f;

    [Range(0.0f, 1.0f)]
    public float CapsulePercentLineWidth = 1.0f;


    private LineRenderer lineRenderer;
    private CapsuleCollider myCollider;


    private bool reEnableCollider = true;

    private float time;

    protected override void OnSpawn()
    {

        gameObject.SetActive(true);

        if(lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        if (myCollider == null)
            myCollider = GetComponent<CapsuleCollider>();

   
        myCollider.center = Vector3.zero;
        myCollider.direction = 2;

        SetLineWidth(lineWidth);

        lineRenderer.SetPosition(0, spawnTransform.position);
        lineRenderer.SetPosition(1, targetTransform.position);
        UpdateCapsule();

        reEnableCollider = true;

        damage = Setting.TESLA_TOWER_DEFAULT_DAMAGE_PER_ATTACK;
    }

    void SetLineWidth(float newWidth)
    {
        lineWidth = newWidth;

        myCollider.radius = (lineWidth / 2.0f) * CapsulePercentLineWidth;
        lineRenderer.SetWidth(lineWidth, lineWidth);
    }

    void Update()
    {
        if (targetTransform)
        {
            lineRenderer.SetPosition(0, spawnTransform.position);
            lineRenderer.SetPosition(1, targetTransform.position);

            time += Time.deltaTime * LineWidthChangeSpeed;
            SetLineWidth((Mathf.Sin(time) * 0.5f + 1.0f + MinLineWidth) * (MaxLineWidth - MinLineWidth));

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
        if (targetTransform && reEnableCollider)
        {
            myCollider.enabled = true;
            reEnableCollider = false;

        }
          
    }

    void UpdateCapsule()
    {
        myCollider.transform.position = spawnTransform.position + (targetTransform.position - spawnTransform.position) / 2;
        myCollider.transform.LookAt(spawnTransform.position);
        myCollider.height = (spawnTransform.position - targetTransform.position).magnitude;
    }


}
