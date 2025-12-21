using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpPower = 12f;

    [Header("Ice Settings")]
    public float iceAcceleration = 30f;
    public float iceMaxSpeed = 6f;

    Rigidbody2D rigid;

    bool isGrounded;
    bool onIce;

    // ⭐ 스턴 상태
    bool isStunned;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
    }

    void Update()
    {
        // ⛔ 스턴 중에는 점프 입력 차단
        if (isStunned)
            return;

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // ⛔ 스턴 중에는 이동 입력 차단 (중력은 Rigidbody가 처리)
        if (isStunned)
            return;

        float h = Input.GetAxisRaw("Horizontal");

        if (onIce)
        {
            // ❄️ 얼음: 관성 이동
            rigid.AddForce(Vector2.right * h * iceAcceleration);

            // 최대 속도 제한
            if (Mathf.Abs(rigid.linearVelocity.x) > iceMaxSpeed)
            {
                rigid.linearVelocity = new Vector2(
                    Mathf.Sign(rigid.linearVelocity.x) * iceMaxSpeed,
                    rigid.linearVelocity.y
                );
            }
        }
        else
        {
            // 🟫 일반 바닥: 즉각 반응
            rigid.linearVelocity = new Vector2(h * moveSpeed, rigid.linearVelocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥 판정 (Platform / Ice 공통)
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ice"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.7f)
                {
                    isGrounded = true;
                    onIce = collision.gameObject.CompareTag("Ice");
                    return;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ice"))
        {
            isGrounded = false;
            onIce = false;
        }
    }

    // =========================
    // ⭐ 외부(스턴 시스템)에서 호출
    // =========================
    public void SetStun(bool value)
    {
        isStunned = value;

        // 스턴 걸릴 때 가로 속도 제거 (미끄러지다 멈추는 거 방지)
        if (isStunned)
        {
            rigid.linearVelocity = new Vector2(0f, rigid.linearVelocity.y);
        }
    }
}