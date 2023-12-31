using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //적이 가질 수 있는 상태 목록
    public enum EnemyState
    {
        Idle, //기본
        Walk, //이동
        Attack, //공격
        Damaged, //피격
        Dead //죽음
    }

    //상태를 담아둘 변수를 만들고, 기본 상태로 시작
    public EnemyState eState = EnemyState.Idle;
    public Slider hpBar;
    public float hp = 100;

    Transform player;
    NavMeshAgent agent; //NavMeshAgent 컴포넌트
    float distance; //플레이어와의 거리
    
    //공격 받는 기능
    void Damaged(float damage)
    {
        //공격 받은 만큼 체력 감소
        hp -= damage;

        //감소한 체력을 체력바에 표시
        hpBar.value = hp;

        agent.isStopped = true;//이동 중단
        agent.ResetPath();//경로 초기화

        if(hp > 0)//체력 남아있다면
        {
            eState = EnemyState.Damaged; //피격 상태로 전환
        }
        else //남아있지 않으면
        {
            eState = EnemyState.Dead;  //죽음
        }
    }
    void Start()
    {
        //Player 컴포넌트로 찾은 플레이어의 Transform 컴포넌트 가져오기
        player = FindObjectOfType<Player>().transform;

        //나의 NavMeshAgent 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //적과 플레이어 사이의 거리 계산
        distance = Vector3.Distance(transform.position, player.position);        

        //기본 이동, 공격 상태일 때 할 일 나누기
        switch (eState)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Walk: Walk(); break;
            case EnemyState.Attack: Attack(); break;
        }
    }
    void Idle() 
    {
        //플레이어와의 거리가 8 이하라면
        if(distance < 8)
        {
            eState = EnemyState.Walk; //이동 상태로 전환
            agent.isStopped = false; //이동시작
        }
    }

    void Walk()
    {
        //플레이어와의 거리가 8 이상이라면
        if (distance > 8) {
            eState = EnemyState.Idle; //기본 상태로 전환
            agent.isStopped = true; //이동 중단
            agent.ResetPath(); //경로 초기화
        }
        //거리가 2 이하라면
        else if (distance <= 2) 
        {
            eState = EnemyState.Attack; //공격 상태로 전환
            agent.isStopped = true; //이동 중단
            agent.ResetPath(); //경로 초기화
        }
        //다른 상태로 전환하지 않을 때는
        else
        {
            //플레이어의 위치를 목적지로 설정
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        //플레이어와의 거리가 2 이상이라면
        if (distance > 2)
        {
            eState = EnemyState.Walk; //이동 상태로 전환
            agent.isStopped = false; //이동 시작
        }
    }
}
