using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float minDistance = 5;      // ī�޶� ���� �ּ� �Ÿ�
    private float maxDistance = 15;     // ī�޶� ���� �ִ� �Ÿ�
    private float wheelSpeed = 500;     // ī�޶� �� �ӵ�
    private float xMoveSpeed = 500;     // ���콺 �¿� �̵� �ӵ�
    private float yMoveSpeed = 250;     // ���콺 ���Ʒ� �̵� �ӵ�
    private float yMinLimit = 10;       // ī�޶� �Ʒ����� �̵� �ִ�ġ
    private float yMaxLimit = 100;      // ī�޶� ������ �̵� �ִ�ġ
    private float x, y;                 // Update()������ �̵� ��ų x, y �� ���� ���� ����
    [HideInInspector]
    public float distance;              // ī�޶� �̵��� �Ÿ�

    public bool isOnShake { set; get; }

    void Awake()
    {
        distance = Vector3.Distance(transform.position, target.position); // �÷��̾�� ī�޶� ������ �� ���

        Vector3 angles = transform.position; // ���� ��ġ �� �����ϰ� x, y ���� ���� ����
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        if (target == null) return;     // Ÿ���� ���ٸ� ����

        if (isOnShake == true) return;  // ī�޶� ��鸲 ȿ���� ���� ���� ����

        x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;    // ���콺 �¿� ������(up����) * �̵��ӵ� * �ʴ� ������ Ƚ�� 
        y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;    // ���콺 ���� ������(right����) * �̵��ӵ� * �ʴ� ������ Ƚ�� -> ��ȸ���� �ݴ�� �������� -= ����

        y = ClampAngle(y, yMinLimit, yMaxLimit);    // ���콺 ���Ʒ� �ִ�ġ ���� 

        transform.rotation = Quaternion.Euler(y, x, 0);    // ���Ϸ����� ���ʹϾ����� ��ȯ���� transform.rotation�� ���� 

        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;   // ���콺 ��ũ�� �� * �ӵ� * �ʴ� ������ Ƚ��
        distance = Mathf.Clamp(distance, minDistance, maxDistance);     // ���� �ִ� �Ÿ��� �ּ� �Ÿ� ���
    }

    private void LateUpdate()
    {
        if (target == null) return;

        if (isOnShake == true) return;

        transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;   // ī�޶��� ��ġ = (������ ȸ�� == ���ʹϾ� * ����) + ����Ʈ
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
