using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpPower = 12f;

    Rigidbody2D rigid;

    bool isGrounded;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.freezeRotation = true;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0); // 점프 안정화
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        // Jump King 스타일 이동 (velocity 직접 제어)
        rigid.linearVelocity = new Vector2(h * moveSpeed, rigid.linearVelocity.y);
    }

    // ⭐ 착지 판정
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Platform"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // 위에서 밟았을 때만 착지
            if (contact.normal.y > 0.7f)
            {
                isGrounded = true;
                break;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
{
    if (!collision.gameObject.CompareTag("Platform"))
        return;

    foreach (ContactPoint2D contact in collision.contacts)
    {
        if (contact.normal.y > 0.7f)
        {
            isGrounded = true;
            return;
        }
    }

    isGrounded = false;
}
}