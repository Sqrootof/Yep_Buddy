using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPYPAchievement : MonoBehaviour
{
    AchievementManager achievementManager;
    private void Start()
    {
        achievementManager = GetComponent<AchievementManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            // �ҵ���Ӧ�ɾ͵�����
            int achievementIndex = Whole.achievements.FindIndex(a => a.name == "��һ���ɾ�");

            // ���ɾ��Ƿ������δ����
            if (achievementIndex != -1 && Whole.achievements[achievementIndex].isUnlocked == false)
            {
                achievementManager.UnlockAchievement("��һ���ɾ�");
            }
        }
    }
}
