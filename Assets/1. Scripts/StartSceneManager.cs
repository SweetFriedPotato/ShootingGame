using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    //Start 버튼을 클릭하면 호출
    public void OnClickStart()
    {
        //2. PlayScene라는 이름의 씬 불러오기
        SceneManager.LoadScene("2. PlayScene");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
