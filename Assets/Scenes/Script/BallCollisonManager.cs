using UnityEngine;

public class BallCollisionManager : MonoBehaviour
{
    public GameObject tennisBallPrefab;   // Tennis 공의 프리팹
    public GameObject baseballBallPrefab; // Baseball 공의 프리팹
    public GameObject footballBallPrefab; // Football 공의 프리팹
    public GameObject volleyballBallPrefab; // Volleyball 공의 프리팹
    public GameObject soccerBallPrefab;   // Soccer 공의 프리팹
    public GameObject basketballBallPrefab; // Basketball 공의 프리팹
    public GameObject golfBallPrefab;     // Golf 공의 프리팹

    private bool hasCollided = false; // 충돌 처리 여부를 체크

    private void OnCollisionEnter(Collision collision)
    {
        // 이미 충돌 처리된 경우 더 이상 실행하지 않도록 방지
        if (hasCollided || collision.gameObject.GetInstanceID() < gameObject.GetInstanceID()) return;

        // 충돌한 두 공의 타입에 따라서 다른 처리
        if (gameObject.CompareTag("Golf") && collision.gameObject.CompareTag("Golf"))
        {
            HandleGolfCollision(collision);
        }
        else if (gameObject.CompareTag("Tennis") && collision.gameObject.CompareTag("Tennis"))
        {
            HandleTennisCollision(collision);
        }
        else if (gameObject.CompareTag("Base") && collision.gameObject.CompareTag("Base"))
        {
            HandleBaseballCollision(collision);
        }
        else if (gameObject.CompareTag("Volley") && collision.gameObject.CompareTag("Volley"))
        {
            HandleVolleyballCollision(collision);
        }
        else if (gameObject.CompareTag("Foot") && collision.gameObject.CompareTag("Foot"))
        {
            HandleFootballCollision(collision);
        }
        else if (gameObject.CompareTag("Soccer") && collision.gameObject.CompareTag("Soccer"))
        {
            HandleSoccerCollision(collision);
        }
        else if (gameObject.CompareTag("Basket") && collision.gameObject.CompareTag("Basket"))
        {
            HandleBasketballCollision(collision);
        }
    }

    private void HandleGolfCollision(Collision collision)
    {
        hasCollided = true; // 충돌 처리 완료
        Destroy(gameObject);
        Destroy(collision.gameObject);
        Vector3 midPoint = (transform.position + collision.transform.position) / 2;
        Instantiate(tennisBallPrefab, midPoint, Quaternion.identity);
    }

    private void HandleTennisCollision(Collision collision)
    {
        hasCollided = true; // 충돌 처리 완료
        Destroy(gameObject);
        Destroy(collision.gameObject);
        Vector3 midPoint = (transform.position + collision.transform.position) / 2;
        Instantiate(baseballBallPrefab, midPoint, Quaternion.identity); // 야구공 생성
    }

    private void HandleBaseballCollision(Collision collision)
    {
        hasCollided = true; // 충돌 처리 완료
        Destroy(gameObject);
        Destroy(collision.gameObject);
        Vector3 midPoint = (transform.position + collision.transform.position) / 2;
        Instantiate(volleyballBallPrefab, midPoint, Quaternion.identity);
    }

    private void HandleVolleyballCollision(Collision collision)
    {
        hasCollided = true; // 충돌 처리 완료
        Destroy(gameObject);
        Destroy(collision.gameObject);
        Vector3 midPoint = (transform.position + collision.transform.position) / 2;
        Instantiate(footballBallPrefab, midPoint, Quaternion.identity);
    }

    private void HandleFootballCollision(Collision collision)
    {
        hasCollided = true; // 충돌 처리 완료
        Destroy(gameObject);
        Destroy(collision.gameObject);
        Vector3 midPoint = (transform.position + collision.transform.position) / 2;
        Instantiate(soccerBallPrefab, midPoint, Quaternion.identity);
    }

    private void HandleSoccerCollision(Collision collision)
    {
        hasCollided = true; // 충돌 처리 완료
        Destroy(gameObject);
        Destroy(collision.gameObject);
        Vector3 midPoint = (transform.position + collision.transform.position) / 2;
        Instantiate(basketballBallPrefab, midPoint, Quaternion.identity);
    }

    private void HandleBasketballCollision(Collision collision)
    {
        hasCollided = true; // 충돌 처리 완료
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
