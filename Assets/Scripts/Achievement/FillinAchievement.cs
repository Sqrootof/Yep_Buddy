using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillinAchievement : MonoBehaviour
{
    public List<Achievement> achievements = new List<Achievement>(); //�ɾ��б�
    // Start is called before the first frame update
    void Start()
    {
        if (Whole.firstLogin == 0)
        {
            FillAchievement();
            Whole.firstLogin++;
        }
        else
        {
            LoadAchievements();
        }
    }

    void FillAchievement()
    {
        Whole.achievements.AddRange(achievements);
    }

    public void LoadAchievements()
    {
        // ��PlayerPrefs�м��سɾ�����
        string json = PlayerPrefs.GetString("Achievements", "[]");
        List<Achievement> loadedAchievements = JsonUtility.FromJson<AchievementList>(json).achievements;

        // ����Whole.achievements�б�
        Whole.achievements = loadedAchievements;
    }
}
