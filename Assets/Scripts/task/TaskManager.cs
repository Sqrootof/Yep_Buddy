using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> questList = new List<Task>(); // �����б�
    private Task trackedTask = null; // ��ǰ��׷�ٵ�����

    // �������
    public void AddQuest(Task task)
    {
        questList.Add(task);
    }

    // �л�����׷��״̬
    public void ToggleTracking(Task task)
    {
        if (task.isTracking)
        {
            // ȡ��׷��
            task.isTracking = false;
            trackedTask = null;
            Debug.Log("Tracking canceled for: " + task.taskName);
        }
        else
        {
            // ����Ѿ���һ��������׷�٣���ȡ������׷��
            if (trackedTask != null)
            {
                trackedTask.isTracking = false;
            }

            // ׷�ٵ�ǰ����
            task.isTracking = true;
            trackedTask = task;
            Debug.Log("Tracking task: " + task.taskName);
        }
    }
}