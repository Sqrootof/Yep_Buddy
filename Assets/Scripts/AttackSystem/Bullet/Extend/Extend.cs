using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AOT;
using Sirenix.Serialization;

[CreateAssetMenu(fileName = "NewExtend", menuName = "Data/Bullet/Extend", order = 2)]
public class Extend : Bullet
{
    public new BulletType BulletType{
        get => BulletType.Extend;
    }

    public int StepExtension = 1;//ÿ���ڻ�ȡ�ӵ�ʱ֧����չ�ĵ��ӵ��鳤��
}
