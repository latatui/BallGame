using UnityEngine;

public class BallManager : MonoBehaviour
{
    [System.Serializable]
    public class BallType
    {
        public string name;            // 공 이름
        public GameObject prefab;      // 공 프리팹
        public float spawnChance;      // 소환 확률
    }

    public BallType[] ballTypes;           // 공 종류 배열
    public Transform spawnPoint;           // 현재 공의 위치 (0, 3, 0)
    public Transform nextBallPoint;        // 다음 공의 위치 (0, 3, 3)
    public float moveSpeed = 5f;           // 공 이동 속도
    public float boundary = 10f;           // 공 이동 제한 범위
    public float fixedY = 3f;              // 공의 고정 Y 좌표

    private GameObject currentBall;        // 현재 조작 중인 공
    private BallType nextBallType;         // 다음 공의 정보 (프리팹은 저장하지 않음)
    private bool isFalling = false;        // 공이 떨어지는 상태

    void Start()
    {
        Debug.Log("게임 시작: SpawnNextBall 호출");
        SpawnNextBall();        // 첫 번째 "다음 공" 정보 저장

        Debug.Log("SpawnNextBall 완료, SpawnCurrentBall 호출");
        SpawnCurrentBall();     // 첫 번째 "현재 공" 배치
    }

    void Update()
    {
        // 공이 떨어지는 중이면 조작 불가
        if (isFalling || currentBall == null) return;

        // 공 이동 (WASD 입력 처리)
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // W, S
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // A, D
        currentBall.transform.position += new Vector3(moveX, 0, moveZ);

        // 이동 제한 (경계를 벗어나지 않도록 제한)
        currentBall.transform.position = new Vector3(
            Mathf.Clamp(currentBall.transform.position.x, -boundary, boundary),
            fixedY, // 항상 고정된 Y 좌표 유지
            Mathf.Clamp(currentBall.transform.position.z, -boundary, boundary)
        );

        // F 키로 공 떨어뜨리기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFalling();
        }
    }

    private void StartFalling()
    {
        isFalling = true;
        Debug.Log("StartFalling: 현재 공 떨어짐");

        // Rigidbody 추가 및 중력 활성화
        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = currentBall.AddComponent<Rigidbody>();
            Debug.Log("StartFalling: Rigidbody 추가");
        }
        rb.useGravity = true;

        // Y 위치 고정 해제
        rb.constraints = RigidbodyConstraints.None;

        // 1초 후 다음 공 준비
        Invoke(nameof(SpawnCurrentBall), 1f);
    }

    private void SpawnCurrentBall()
    {
        // nextBall에서 공의 프리팹을 가져와 currentBall로 설정
        if (nextBallType != null)
        {
            currentBall = Instantiate(nextBallType.prefab, spawnPoint.position, Quaternion.identity);
            Debug.Log($"SpawnCurrentBall: 새로운 currentBall 생성 - {nextBallType.name}");
        }

        // 새로운 공 Y 좌표 고정
        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Destroy(rb); // Rigidbody 제거
            Debug.Log("SpawnCurrentBall: Rigidbody 제거 완료");
        }

        // 다음 공 준비
        Debug.Log("SpawnCurrentBall: 다음 공 생성 호출");
        SpawnNextBall();

        isFalling = false; // 떨어지는 상태 해제
        Debug.Log("SpawnCurrentBall: isFalling 상태 초기화");
    }

    private void SpawnNextBall()
    {
        // 확률에 따라 공 선택
        nextBallType = GetRandomBallType();
        Debug.Log($"SpawnNextBall: 새로운 nextBall 정보 저장 - {nextBallType.name}");

        // 다음 공은 생성하지 않음, 정보만 저장
        // nextBall = Instantiate(selectedBallType.prefab, nextBallPoint.position, Quaternion.identity); // 이 줄을 주석 처리함으로써 공 생성 방지
        // Rigidbody 제거 또는 비활성화
        // Rigidbody rb = nextBall.GetComponent<Rigidbody>();
        // if (rb != null)
        // {
        //     Destroy(rb); // nextBall은 중력 영향을 받지 않도록 처리
        //     Debug.Log("SpawnNextBall: nextBall의 Rigidbody 제거");
        // }
    }

    private BallType GetRandomBallType()
    {
        float totalChance = 0f;
        foreach (var ballType in ballTypes)
        {
            totalChance += ballType.spawnChance;
        }

        float randomValue = Random.Range(0, totalChance);
        float cumulativeChance = 0f;

        foreach (var ballType in ballTypes)
        {
            cumulativeChance += ballType.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                Debug.Log($"GetRandomBallType: 선택된 공 - {ballType.name}");
                return ballType;
            }
        }

        Debug.LogWarning("GetRandomBallType: 기본값으로 첫 번째 공 반환");
        return ballTypes[0]; // 기본값 (문제가 있을 경우)
    }
}
