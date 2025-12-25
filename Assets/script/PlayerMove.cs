using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpPower = 12f;

    [Header("Ice Settings")]
    public float iceAcceleration = 30f;
    public float iceMaxSpeed = 6f;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sr;   // ⭐ 추가

    bool isGrounded;
    bool onIce;

    // ⭐ 스턴 상태
    bool isStunned;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();   // ⭐ 추가
    }

    void Update()
    {
        // Animator 스턴 상태 전달
        anim.SetBool("isStunned", isStunned);

        if (isStunned)
        {
            anim.SetBool("isWalking", false);
            return;
        }

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
        if (isStunned)
            return;

        float h = Input.GetAxisRaw("Horizontal");

        // ⭐ 이동 애니메이션
        anim.SetBool("isWalking", h != 0);

        // ⭐ 방향 전환 (왼쪽 보면 뒤집기)
        if (h != 0)
        {
            sr.flipX = h < 0;
        }

        if (onIce)
        {
            rigid.AddForce(Vector2.right * h * iceAcceleration);

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
            rigid.linearVelocity = new Vector2(h * moveSpeed, rigid.linearVelocity.y);
        }
    }

    // 착지 판정
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Platform") &&
            !collision.gameObject.CompareTag("Ice"))
            return;

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

    // ⭐ 보정용
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Platform") &&
            !collision.gameObject.CompareTag("Ice"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.7f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    // Exit에서는 grounded를 끊지 않음
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            onIce = false;
        }
    }

    // =========================
    // ⭐ 외부(스턴 시스템)에서 호출
    // =========================
    public void SetStun(bool value)
    {
        isStunned = value;

        if (isStunned)
        {
            rigid.linearVelocity = new Vector2(0f, rigid.linearVelocity.y);
        }
    }
}