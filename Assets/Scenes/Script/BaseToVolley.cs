using UnityEngine;

public class BaseToVolley : MonoBehaviour
{
    public GameObject VolleyBallPrefab;  // 야구공 프리팹

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 Tennis 공인지 확인
        if (collision.gameObject.CompareTag("Base") && gameObject.CompareTag("Base"))
        {
            // 두 오브젝트의 중간 위치 계산
            Vector3 midPoint = (transform.position + collision.transform.position) / 2;
            
            // 충돌한 두 공을 삭제
            Destroy(gameObject);
            Destroy(collision.gameObject);
            
            // 중간 지점에 BaseBall (야구공) 생성
            Instantiate(VolleyBallPrefab, midPoint, Quaternion.identity);
        }
    }
}
