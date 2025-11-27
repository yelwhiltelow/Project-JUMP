using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;   // 플레이어 이동 속도
    public float jumpForce = 7.0f;   // 점프 힘

    float horizontal;                // 좌우 입력값
    Rigidbody2D rb;                  // Rigidbody2D 참조
    bool isGrounded;                 // 땅에 닿아있는지 체크

    public Transform groundCheck;    // 땅 체크용 Transform (발밑 위치)
    public LayerMask groundLayer;    // 땅 레이어 지정

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        CheckGround();
    }

    private void Update()
    {
        Jump();
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void CheckGround()
    {
        // 발밑에 작은 원을 그려서 땅과 닿아있는지 확인
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}


