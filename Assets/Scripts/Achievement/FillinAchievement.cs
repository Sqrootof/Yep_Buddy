using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillinAchievement : MonoBehaviour
{
    public List<Achievement> achievements = new List<Achievement>(); //成就列表
    // Start is called before the first frame update
    void Start()
    {
        //读取和保存成就
        Whole.firstLogin = PlayerPrefs.GetInt("firstLogin");

        if (Whole.firstLogin == 0)
        {
            FillAchievement();
            Whole.firstLogin++;
            PlayerPrefs.SetInt("firstLogin", Whole.firstLogin);
            PlayerPrefs.Save();
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
        // 从PlayerPrefs中加载成就数据
        string json = PlayerPrefs.GetString("Achievements", "[]");
        List<Achievement> loadedAchievements = JsonUtility.FromJson<AchievementList>(json).achievements;

        // 更新Whole.achievements列表
        Whole.achievements = loadedAchievements;
    }
}
