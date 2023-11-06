using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public float rotateSpeed;

    //eulerangles.x 담아둘 변수
    float tempX;

    void Update()
    {
        //마우스의 위아래 움직임 입력을 숫자로 받아서 저장
        float mouseMoveY = Input.GetAxis("Mouse Y");

        //마우스가 움직인 만큼 x축 회전
        transform.Rotate(-mouseMoveY * rotateSpeed * Time.deltaTime, 0, 0);

        //x의 각도가 180 넘으면
        if(transform.eulerAngles.x  > 180)
        {
            tempX = transform.eulerAngles.x - 360;
        }
        else
        {
            tempX = transform.eulerAngles.x;
        }

        //음수 포함한 x의 각도를 -30 ~ 30으로 제한
        tempX = Mathf.Clamp(tempX, -30, 30);

        //제한된 값을 eulerangles.x에 적용 (y축과 z축은 고정되지 않음, 현재 각도대로)
        transform.eulerAngles = new Vector3(tempX, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
