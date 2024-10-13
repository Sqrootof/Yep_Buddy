using UnityEngine;

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed
}

[System.Serializable]
public class Task
{
    public string taskName; // ��������
    public string description; // ��������
    public Vector3 trackingCoordinates; // ׷�ٵ�����

    public bool isAccepted = false; // �Ƿ��ѽ�ȡ
    public bool isCompleted = false; // �Ƿ������
    public bool isTracking = false; // �Ƿ����ڱ�׷��

    public TaskStatus status = TaskStatus.NotStarted;

    public void AcceptTask()
    {
        isAccepted = true;
        status = TaskStatus.InProgress;
        Debug.Log("Task accepted: " + taskName);
    }

    public void CompleteQuest()
    {
        isCompleted = true;
        status = TaskStatus.Completed;
        Debug.Log("Task completed: " + taskName);
    }

    // ��������Ƿ���ʾ��������
    public bool ShouldDisplayInUI()
    {
        return isAccepted && !isCompleted;
    }

    // �ж��Ƿ���������� (���Ը����������ж���������)
    public bool CanComplete()
    {
        // ��������ж��߼�
        return isCompleted;
    }
}
