using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Move Settings")]
    public float moveDistance = 5f;   // 좌우 이동 거리
    public float moveSpeed = 2f;      // 이동 속도

    Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float xOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = startPos + Vector3.right * xOffset;
    }
}