using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    public Transform player;   // 따라갈 플레이어
    public float minY = 0f;     // 바닥 고정 Y
    public float maxY = 20f;    // 최고 고정 Y

    void LateUpdate()
    {
        float playerY = player.position.y;

        // 플레이어 y를 minY ~ maxY 사이로 제한
        float clampedY = Mathf.Clamp(playerY, minY, maxY);

        // 카메라 위치 적용 (x, z는 유지)
        transform.position = new Vector3(
            transform.position.x,
            clampedY,
            transform.position.z
        );
    }
}