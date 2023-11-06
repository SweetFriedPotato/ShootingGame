using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;//�̵� �ӵ�
    public float rotateSpeed; //ȸ�� �ӵ�
    public float jumpPower;//�����ϴ� ��

    int jumpCount; //������ Ƚ��

    Rigidbody rb; //�÷��̾��� rigidbody ������Ʈ

    void Start()
    {
        //�÷��̾��� rigidbody ������Ʈ �����ͼ� ����
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //����Ű �Ǵ� WASDŰ �Է��� ���ڷ� �޾Ƽ� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        //x�࿡�� h�� ����, y�࿡�� v�� ���� ��� ���ϱ�
        Vector3 dir = new Vector3(h, 0, v);

        //��� ������ �ӵ��� �����ϵ��� ����ȭ
        dir.Normalize();

        //�÷��̾� �������� dir�� ���� ���� (�ٶ󺸴� �������� �̵�)
        dir = transform.TransformDirection(dir);

        ////�̵��� ���⿡ ���ϴ� �ӵ� ���ϱ�
        //transform.position += dir * moveSpeed * Time.deltaTime;

        //���� �ۿ��� ���� �̵�
        rb.MovePosition(rb.position + ( dir * moveSpeed * Time.deltaTime));

        //space Ű�� ������ ����
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            //���� �������� �� �߻�
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            jumpCount++; //����Ƚ�� ī��Ʈ ����
        }

        //���콺�� �¿� ������ �Է��� ���ڷ� �޾Ƽ� ����
        float mouseMoveX = Input.GetAxis("Mouse X");

        //���콺�� ������ ��ŭ Y�� ȸ��
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�浹�� ��ü�� �±װ� Ground ���
        if(collision.gameObject.tag == "Ground")
        {
            //���� Ƚ�� �ʱ�ȭ
            jumpCount = 0;
        }
    }
}
