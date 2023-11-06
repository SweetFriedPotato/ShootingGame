using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFire : MonoBehaviour
{
    //총알 프리팹을 담아둘 변수
    public GameObject shootEffectPref;

    void Start()
    {
        Cursor.visible = false;

        //화면 밖으로 커서 못 나가게
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //마우스 좌클릭을 누르는 순간
        if (Input.GetMouseButtonDown(0))
        {
            /* 총알 발사 코드
            //게임 안에 총알 프리팹의 복사본 생성(플레이어의 위치보다 1 앞에)
            //생성 후 bullet 변수에 할당
            GameObject bullet = Instantiate(bulletPref, transform.position + transform.forward, Quaternion.identity);

            //총알 복사본이 앞으로 날아가는 순간적 힘 발생
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
            */

            //화면 가운데에서 시작하는 ray 생성
            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

            //Ray 에 맞은 물체를 담아둘 변수
            RaycastHit hit;

            //ray를 발사하고,  ray에 맞은 물체는 hit 에 저장, 맞은 물체가 있을 때만
            if(Physics.Raycast(ray, out hit))
            {
                //맞은 위치에 맞은 표면의 수직이 되는 각도로 총 효과 프리팹의 복사본 생성
                GameObject shootEffect = Instantiate(shootEffectPref, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));

                //총알 자국을 맞은 오브젝트의 자식으로 설정
                shootEffect.transform.SetParent(hit.transform);

                //Ray 에 맞은 물체가 적이라면
                if(hit.transform.tag == "Enemy")
                {
                    //적에게 10만큼 공격받으라고 전달
                    hit.transform.SendMessage("Damaged", 10);
                }
            } 
        }
    }
}
