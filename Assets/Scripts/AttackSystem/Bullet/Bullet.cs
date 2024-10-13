using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Projectile,//��ͨ�䵯
    Extend,//��չ�䵯
    Gain//�䵯����
}

public class Bullet : ScriptableObject
{
    public string BulletName;
    public Sprite Icon;
    public BulletType BulletType;
}
