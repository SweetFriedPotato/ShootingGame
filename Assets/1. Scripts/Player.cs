using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;//이동 속도
    public float rotateSpeed; //회전 속도
    public float jumpPower;//점프하는 힘

    int jumpCount; //점프한 횟수

    Rigidbody rb; //플레이어의 rigidbody 컴포넌트

    void Start()
    {
        //플레이어의 rigidbody 컴포넌트 가져와서 저장
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //방향키 또는 WASD키 입력을 숫자로 받아서 저장
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        //x축에는 h의 값을, y축에는 v의 값을 계속 더하기
        Vector3 dir = new Vector3(h, 0, v);

        //모든 방향의 속도가 동일하도록 정규화
        dir.Normalize();

        //플레이어 기준으로 dir의 방향 조절 (바라보는 방향으로 이동)
        dir = transform.TransformDirection(dir);

        ////이동할 방향에 원하는 속도 곱하기
        //transform.position += dir * moveSpeed * Time.deltaTime;

        //물리 작용을 통해 이동
        rb.MovePosition(rb.position + ( dir * moveSpeed * Time.deltaTime));

        //space 키를 누르는 순간
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            //위로 순간적인 힘 발생
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            jumpCount++; //점프횟수 카운트 증가
        }

        //마우스의 좌우 움직임 입력을 숫자로 받아서 저장
        float mouseMoveX = Input.GetAxis("Mouse X");

        //마우스가 움직인 만큼 Y축 회전
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 물체의 태그가 Ground 라면
        if(collision.gameObject.tag == "Ground")
        {
            //점프 횟수 초기화
            jumpCount = 0;
        }
    }
}
