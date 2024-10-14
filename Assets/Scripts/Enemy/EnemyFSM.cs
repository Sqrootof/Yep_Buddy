using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬�����ƽű�
/// </summary>
public class EnemyFSM
{
    public EnemyState currentState; // ��ǰ״̬
    public EnemyState startState;   // ��ʼ״̬

    /// <summary>
    /// ״̬��ʼ����������OnEnable�е���
    /// </summary>
    /// <param name="state">��ʼ״̬</param>
    public void InitializeState(EnemyState state)
    {
        currentState = state; // ����ǰ״̬����Ϊ��ʼ״̬
        currentState.OnEnter(); // ִ�е�ǰ״̬��OnEnter����
    }

    /// <summary>
    /// �л�״̬����
    /// </summary>
    /// <param name="state">�л����״̬</param>
    public void ChangeState(EnemyState state)
    {
        currentState.OnExit(); // ִ�е�ǰ״̬��OnExit����
        currentState = state; // ����ǰ״̬����Ϊ��״̬
        currentState.OnEnter(); // ִ����״̬��OnEnter����
    }
}
