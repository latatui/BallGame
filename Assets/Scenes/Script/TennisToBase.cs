using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisToBase : MonoBehaviour
{
    public GameObject BaseBallPrefab;  // Tennis 공의 프리팹

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 Golf 공인지 확인
        if (collision.gameObject.CompareTag("Tennis"))
        {
            // Golf 공이 두 개 충돌한 경우
            if (gameObject.CompareTag("Tennis"))
            {
                // 두 오브젝트의 중간 위치 계산
                Vector3 midPoint = (transform.position + collision.transform.position) / 2;
                // 중간 지점에 Tennis 공 생성
                Instantiate(BaseBallPrefab, midPoint, Quaternion.identity);
            }
        }
    }
}
