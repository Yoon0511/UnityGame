using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStome : MonoBehaviour
{
    Vector3 startPoint;
    Vector3 controlPoint;
    Vector3 endPoint;
    public float moveSpeed = 0.3f;

    private float progress = 0f;
    float DistX = 0f;
    float DistZ = 0f;

    public void Init(float _distx,float _distz)
    {
        DistX = _distx;
        DistZ = _distz;

        startPoint = transform.position;

        endPoint.x = startPoint.x + DistX;
        endPoint.z = startPoint.z + DistZ;

        float ControlZ = DistZ * 0.8f;
        float ControlX = DistX * 0.3f;

        controlPoint.z = startPoint.z + ControlZ;
        controlPoint.x = startPoint.x + ControlX;
    }

    private void FixedUpdate()
    {
        progress += moveSpeed * Time.deltaTime;
        progress = Mathf.Clamp01(progress);
        
        Vector3 m1 = Vector3.Slerp(startPoint, controlPoint, progress);
        Vector3 m2 = Vector3.Slerp(controlPoint, endPoint, progress);
        transform.position = Vector3.Slerp(m1, m2, progress);
        
        if(Vector3.Distance(startPoint, endPoint) <= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
