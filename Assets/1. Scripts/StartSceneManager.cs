using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    //Start ��ư�� Ŭ���ϸ� ȣ��
    public void OnClickStart()
    {
        //2. PlayScene��� �̸��� �� �ҷ�����
        SceneManager.LoadScene("2. PlayScene");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
