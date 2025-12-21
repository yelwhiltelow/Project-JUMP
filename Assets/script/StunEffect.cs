using UnityEngine;

public class StunEffect : MonoBehaviour
{
    [Header("Stun Settings")]
    public float maxStunTime = 5f;
    public float minStunTime = 0f;

    [Header("Move Settings")]
    public float moveSpeed = 3f;

    [HideInInspector] public float minY; // 시작 Y
    [HideInInspector] public float maxY; // 끝 Y (StunBlock에서 설정)

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ⬆️ 위로 이동
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // 위치 보간값 (0 = 아래, 1 = 위)
        float t = Mathf.InverseLerp(minY, maxY, transform.position.y);

        // 투명도
        Color c = sr.color;
        c.a = Mathf.Lerp(1f, 0f, t);
        sr.color = c;

        // 끝까지 올라가면 제거
        if (transform.position.y >= maxY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerStun stun = other.GetComponent<PlayerStun>();
        if (stun == null)
            return;

        // 현재 위치 기반 스턴 시간 계산
        float t = Mathf.InverseLerp(minY, maxY, transform.position.y);
        float stunTime = Mathf.Lerp(maxStunTime, minStunTime, t);

        if (stunTime > 0f)
        {
            stun.ApplyStun(stunTime);
        }
    }
}