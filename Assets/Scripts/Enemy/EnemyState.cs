using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬������
/// </summary>
public abstract class EnemyState
{
    protected Enemy enemy;
    protected EnemyFSM enemyFSM;

    public EnemyState(Enemy enemy, EnemyFSM enemyFSM)
    {
        this.enemy = enemy;
        this.enemyFSM = enemyFSM;
    }

    public abstract void OnEnter();  //����״̬ʱ����
    public abstract void LogicUpdate();  //״̬���߼����£���Update�����
    public abstract void PhysicsUpdate();    //״̬��������£���FixedUpdate�����
    public abstract void OnExit();   //�˳�״̬ʱ����
}
