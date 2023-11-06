using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFire : MonoBehaviour
{
    //�Ѿ� �������� ��Ƶ� ����
    public GameObject shootEffectPref;

    void Start()
    {
        Cursor.visible = false;

        //ȭ�� ������ Ŀ�� �� ������
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //���콺 ��Ŭ���� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            /* �Ѿ� �߻� �ڵ�
            //���� �ȿ� �Ѿ� �������� ���纻 ����(�÷��̾��� ��ġ���� 1 �տ�)
            //���� �� bullet ������ �Ҵ�
            GameObject bullet = Instantiate(bulletPref, transform.position + transform.forward, Quaternion.identity);

            //�Ѿ� ���纻�� ������ ���ư��� ������ �� �߻�
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
            */

            //ȭ�� ������� �����ϴ� ray ����
            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

            //Ray �� ���� ��ü�� ��Ƶ� ����
            RaycastHit hit;

            //ray�� �߻��ϰ�,  ray�� ���� ��ü�� hit �� ����, ���� ��ü�� ���� ����
            if(Physics.Raycast(ray, out hit))
            {
                //���� ��ġ�� ���� ǥ���� ������ �Ǵ� ������ �� ȿ�� �������� ���纻 ����
                GameObject shootEffect = Instantiate(shootEffectPref, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));

                //�Ѿ� �ڱ��� ���� ������Ʈ�� �ڽ����� ����
                shootEffect.transform.SetParent(hit.transform);

                //Ray �� ���� ��ü�� ���̶��
                if(hit.transform.tag == "Enemy")
                {
                    //������ 10��ŭ ���ݹ������ ����
                    hit.transform.SendMessage("Damaged", 10);
                }
            } 
        }
    }
}
