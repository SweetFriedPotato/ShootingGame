using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public float rotateSpeed;

    //eulerangles.x ��Ƶ� ����
    float tempX;

    void Update()
    {
        //���콺�� ���Ʒ� ������ �Է��� ���ڷ� �޾Ƽ� ����
        float mouseMoveY = Input.GetAxis("Mouse Y");

        //���콺�� ������ ��ŭ x�� ȸ��
        transform.Rotate(-mouseMoveY * rotateSpeed * Time.deltaTime, 0, 0);

        //x�� ������ 180 ������
        if(transform.eulerAngles.x  > 180)
        {
            tempX = transform.eulerAngles.x - 360;
        }
        else
        {
            tempX = transform.eulerAngles.x;
        }

        //���� ������ x�� ������ -30 ~ 30���� ����
        tempX = Mathf.Clamp(tempX, -30, 30);

        //���ѵ� ���� eulerangles.x�� ���� (y��� z���� �������� ����, ���� �������)
        transform.eulerAngles = new Vector3(tempX, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
