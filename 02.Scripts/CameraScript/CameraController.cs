using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float minDistance = 5;      // 카메라 줌인 최소 거리
    private float maxDistance = 15;     // 카메라 줌인 최대 거리
    private float wheelSpeed = 500;     // 카메라 휠 속도
    private float xMoveSpeed = 500;     // 마우스 좌우 이동 속도
    private float yMoveSpeed = 250;     // 마우스 위아래 이동 속도
    private float yMinLimit = 10;       // 카메라 아래방향 이동 최대치
    private float yMaxLimit = 100;      // 카메라 윗방향 이동 최대치
    private float x, y;                 // Update()문에서 이동 시킬 x, y 값 저장 변수 선언
    [HideInInspector]
    public float distance;              // 카메라가 이동할 거리

    public bool isOnShake { set; get; }

    void Awake()
    {
        distance = Vector3.Distance(transform.position, target.position); // 플레이어와 카메라 사이의 값 계산

        Vector3 angles = transform.position; // 현재 위치 값 저장하고 x, y 값에 각각 저장
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        if (target == null) return;     // 타겟이 없다면 리턴

        if (isOnShake == true) return;  // 카메라 흔들림 효과가 있을 때는 리턴

        x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;    // 마우스 좌우 움직임(up벡터) * 이동속도 * 초당 프레임 횟수 
        y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;    // 마우스 상하 움직임(right벡터) * 이동속도 * 초당 프레임 횟수 -> 축회전과 반대로 움직여서 -= 연산

        y = ClampAngle(y, yMinLimit, yMaxLimit);    // 마우스 위아래 최대치 각도 

        transform.rotation = Quaternion.Euler(y, x, 0);    // 오일러각을 쿼터니언으로 변환시켜 transform.rotation에 전달 

        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;   // 마우스 스크롤 휠 * 속도 * 초당 프레임 횟수
        distance = Mathf.Clamp(distance, minDistance, maxDistance);     // 줌인 최대 거리와 최소 거리 계산
    }

    private void LateUpdate()
    {
        if (target == null) return;

        if (isOnShake == true) return;

        transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;   // 카메라의 위치 = (벡터의 회전 == 쿼터니언 * 벡터) + 디폴트
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
