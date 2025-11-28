using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        rigid.freezeRotation = true;    //이동시 굴러가기 방지
    }

    void Update() {
        //점프
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping")) {    //스페이스바, 점프 애니메이션이 작동중이지 않다면
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);    //Jump 애니메이션 추가
        }

        //멈출 때 속도
        if (Input.GetButtonUp("Horizontal")) {
            //normalized : 벡터 크기를 1로 만든 상태
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.normalized.x * 0.5f, rigid.linearVelocity.y);
        }

        //방향 전환
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;    //-1은 왼쪽 방향

        //애니메이션 전환
        if (Mathf.Abs(rigid.linearVelocity.x) < 0.3)   //정지 상태, Mathf : 수학관련 함수를 제공하는 클래스
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");       //좌,우 A, D

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //오른쪽 속도 조절
        if (rigid.linearVelocity.x > maxSpeed)    //velocity : 리지드바디의 현재 속도
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);   //y축 값을 0으로 하면 멈춤
        //왼쪽 속도 조절
        else if (rigid.linearVelocity.x < maxSpeed*(-1))
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);

        //점프 후 착지시 애니메이션 전환(레이캐스트)
        if (rigid.linearVelocity.y < 0) { //y축 속도 값이 0보다 클때만. 땅에 있으면 레이 표시 X
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); //레이 표시
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); //레이에 닿는 물체
            if (rayHit.collider != null) //레이에 닿는 물체가 있다면
                if (rayHit.distance < 0.5f)
                    anim.SetBool("isJumping", false);
        }
    }
}


