using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private List<Achievement> achievements = new List<Achievement>();

    private void Start()
    {
        LoadAchievements();
    }

    public void UnlockAchievement(string achievementName)
    {
        Achievement achievement = achievements.Find(a => a.name == achievementName);
        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.isUnlocked = true;
            SaveAchievements();
        }
    }

    private void LoadAchievements()
    {
        // 从 PlayerPrefs 或文件加载成就状态
        int count = PlayerPrefs.GetInt("AchievementCount", 0);
        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString($"Achievement_{i}_Name");
            bool isUnlocked = PlayerPrefs.GetInt($"Achievement_{i}_Unlocked") == 1;
            achievements.Add(new Achievement(name) { isUnlocked = isUnlocked });
        }
    }

    private void SaveAchievements()
    {
        PlayerPrefs.SetInt("AchievementCount", achievements.Count);
        for (int i = 0; i < achievements.Count; i++)
        {
            PlayerPrefs.SetString($"Achievement_{i}_Name", achievements[i].name);
            PlayerPrefs.SetInt($"Achievement_{i}_Unlocked", achievements[i].isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public List<Achievement> GetAchievements()
    {
        return achievements;
    }
}
