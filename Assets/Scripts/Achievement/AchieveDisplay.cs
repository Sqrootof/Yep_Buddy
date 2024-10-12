using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public GameObject achievementItemPrefab; // Ԥ��
    public Transform achievementListParent; // �ɾ��б�ĸ�����

    void Start()
    {
        DisplayAchievements();
    }

    void DisplayAchievements()
    {
        // ��ճɾ��б�
        foreach (Transform child in achievementListParent)
        {
            Destroy(child.gameObject);
        }

        // ѭ�������ɾ��б���ʾ
        foreach (Achievement achievement in Whole.achievements)
        {
            GameObject achievementItem = Instantiate(achievementItemPrefab, achievementListParent);
            Image itemImage = achievementItem.GetComponent<Image>();
            Text[] texts = achievementItem.GetComponentsInChildren<Text>();

            // ���óɾ�����
            texts[0].text = achievement.name +":"+ achievement.conditions;

            if (achievement.isUnlocked)
            {
                // ʹ�� Color.white
                itemImage.color = new Color(1f, 1f, 1f, 1f); // Image ��ɫ
                texts[0].color = new Color(0f, 0f, 0f, 1f); // �ı���ɫ��Ϊ��ɫ����͸��
            }
            else
            {
                // ����ѡ�����óɾ�δ��������ɫ�������ɫ
                itemImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Image ��ɫ
                texts[0].color = new Color(0.5f, 0.5f, 0.5f,0.5f); // ����Ϊ��ɫ
            }
        }
    }
}
