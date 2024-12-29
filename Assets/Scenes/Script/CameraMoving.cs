using UnityEngine;

public class CubeCameraController : MonoBehaviour
{
    public Transform cube; // 큐브의 Transform
    public float transitionSpeed = 2f; // 전환 속도
    private int currentSideIndex = 0; // 현재 큐브의 면 인덱스 (0~3)
    private bool isTopView = false; // 위쪽 보기 상태
    private Vector3 targetPosition; // 목표 위치
    private Quaternion targetRotation; // 목표 회전

    void Start()
    {
        if (cube == null)
        {
            Debug.LogError("Cube Transform이 설정되지 않았습니다.");
            return;
        }

        // 초기 상태 설정
        UpdateTargetPositionAndRotation();
        MoveCameraInstant();
    }

    void Update()
    {
        if (cube == null) return;

        // F 키 입력 - 위쪽 보기 전환
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleTopView();
        }

        // 스페이스바 입력 - 공 떨어뜨리기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBallAndRotateCamera();
        }

        // 현재 위치 및 회전을 목표로 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);
    }

    // 위쪽 보기 전환
    private void ToggleTopView()
    {
        isTopView = !isTopView;
        UpdateTargetPositionAndRotation();
    }

    // 공을 떨어뜨리고 카메라를 회전
    private void DropBallAndRotateCamera()
    {
        // 공 떨어뜨리기 (관련 로직 연결 가능)
        Debug.Log("공을 떨어뜨렸습니다.");

        // 위쪽 보기 상태인지 확인
        if (isTopView)
        {
            isTopView = false; // 위쪽 보기 상태 해제
        }

        // 항상 왼쪽으로 회전
        currentSideIndex = (currentSideIndex + 1) % 4;
        UpdateTargetPositionAndRotation();
    }

    // 목표 위치 및 회전 업데이트
    private void UpdateTargetPositionAndRotation()
    {
        if (isTopView)
        {
            // 위쪽 보기: 현재 Y축 회전을 기준으로 위쪽에서 바라보는 상태
            targetPosition = cube.position + new Vector3(0, 14, 0);
            targetRotation = Quaternion.Euler(90, currentSideIndex * 90, 0); // X=90, Y는 현재 방향
        }
        else
        {
            // 정면 보기: Y축 회전 기준으로 위치와 방향 설정
            Vector3[] sideOffsets = new Vector3[]
            {
                new Vector3(0, -2, -20),  // 정면
                new Vector3(-20, -2, 0), // 왼쪽
                new Vector3(0, -2, 20),  // 뒷면
                new Vector3(20, -2, 0)   // 오른쪽
            };

            targetPosition = cube.position + sideOffsets[currentSideIndex];
            targetRotation = Quaternion.Euler(0, currentSideIndex * 90, 0); // X=0, Y는 현재 방향
        }
    }

    // 카메라를 즉시 이동시키는 함수
    private void MoveCameraInstant()
    {
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
