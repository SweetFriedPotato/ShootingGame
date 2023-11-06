using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //���� ���� �� �ִ� ���� ���
    public enum EnemyState
    {
        Idle, //�⺻
        Walk, //�̵�
        Attack, //����
        Damaged, //�ǰ�
        Dead //����
    }

    //���¸� ��Ƶ� ������ �����, �⺻ ���·� ����
    public EnemyState eState = EnemyState.Idle;
    public Slider hpBar;
    public float hp = 100;

    Transform player;
    NavMeshAgent agent; //NavMeshAgent ������Ʈ
    float distance; //�÷��̾���� �Ÿ�
    
    //���� �޴� ���
    void Damaged(float damage)
    {
        //���� ���� ��ŭ ü�� ����
        hp -= damage;

        //������ ü���� ü�¹ٿ� ǥ��
        hpBar.value = hp;

        agent.isStopped = true;//�̵� �ߴ�
        agent.ResetPath();//��� �ʱ�ȭ

        if(hp > 0)//ü�� �����ִٸ�
        {
            eState = EnemyState.Damaged; //�ǰ� ���·� ��ȯ
        }
        else //�������� ������
        {
            eState = EnemyState.Dead;  //����
        }
    }
    void Start()
    {
        //Player ������Ʈ�� ã�� �÷��̾��� Transform ������Ʈ ��������
        player = FindObjectOfType<Player>().transform;

        //���� NavMeshAgent ������Ʈ ��������
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //���� �÷��̾� ������ �Ÿ� ���
        distance = Vector3.Distance(transform.position, player.position);        

        //�⺻ �̵�, ���� ������ �� �� �� ������
        switch (eState)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Walk: Walk(); break;
            case EnemyState.Attack: Attack(); break;
        }
    }
    void Idle() 
    {
        //�÷��̾���� �Ÿ��� 8 ���϶��
        if(distance < 8)
        {
            eState = EnemyState.Walk; //�̵� ���·� ��ȯ
            agent.isStopped = false; //�̵�����
        }
    }

    void Walk()
    {
        //�÷��̾���� �Ÿ��� 8 �̻��̶��
        if (distance > 8) {
            eState = EnemyState.Idle; //�⺻ ���·� ��ȯ
            agent.isStopped = true; //�̵� �ߴ�
            agent.ResetPath(); //��� �ʱ�ȭ
        }
        //�Ÿ��� 2 ���϶��
        else if (distance <= 2) 
        {
            eState = EnemyState.Attack; //���� ���·� ��ȯ
            agent.isStopped = true; //�̵� �ߴ�
            agent.ResetPath(); //��� �ʱ�ȭ
        }
        //�ٸ� ���·� ��ȯ���� ���� ����
        else
        {
            //�÷��̾��� ��ġ�� �������� ����
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        //�÷��̾���� �Ÿ��� 2 �̻��̶��
        if (distance > 2)
        {
            eState = EnemyState.Walk; //�̵� ���·� ��ȯ
            agent.isStopped = false; //�̵� ����
        }
    }
}
