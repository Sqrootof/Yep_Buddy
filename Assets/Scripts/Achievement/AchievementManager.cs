using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    //private void Start()
    //{
    //    LoadAchievements(); // ��Ϸ����ʱ���سɾ�����
    //}

    public void UnlockAchievement(string achievementName)
    {
        // ���Ҷ�Ӧ���Ƶĳɾ�
        Achievement achievement = Whole.achievements.Find(a => a.name == achievementName);

        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.isUnlocked = true; // �����ɾ�
            Debug.Log($"Achievement Unlocked: {achievement.name}");
            SaveAchievements(); // �����󱣴�ɾ�����
        }
    }

    //public void LoadAchievements()
    //{
    //    // ��PlayerPrefs�м��سɾ�����
    //    string json = PlayerPrefs.GetString("Achievements", "[]");
    //    List<Achievement> loadedAchievements = JsonUtility.FromJson<AchievementList>(json).achievements;

    //    // ����Whole.achievements�б�
    //    Whole.achievements = loadedAchievements;
    //}

    public void SaveAchievements()
    {
        // ���ɾ�����תΪJSON�ַ��������浽PlayerPrefs
        string json = JsonUtility.ToJson(new AchievementList { achievements = Whole.achievements });
        PlayerPrefs.SetString("Achievements", json);
        PlayerPrefs.Save(); // ȷ�����ݱ�д��
    }
}

// ���ڴ���Achievement�����л�
[System.Serializable]
public class AchievementList
{
    public List<Achievement> achievements;
}
