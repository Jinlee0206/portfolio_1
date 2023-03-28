using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float minDistance = 5;
    private float maxDistance = 15;
    private float wheelSpeed = 500;
    private float xMoveSpeed = 500;
    private float yMoveSpeed = 250;
    private float yMinLimit = 10;
    private float yMaxLimit = 100;
    private float x, y;
    [HideInInspector]
    public float distance;

    public bool isOnShake { set; get; }

    void Awake()
    {
        distance = Vector3.Distance(transform.position, target.position);

        Vector3 angles = transform.position;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        if (target == null) return;

        if (isOnShake == true) return;

        x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        transform.rotation = Quaternion.Euler(y, x, 0);

        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void LateUpdate()
    {
        if (target == null) return;

        if (isOnShake == true) return;

        transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
