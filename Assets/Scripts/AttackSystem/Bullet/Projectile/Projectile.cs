using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ProjectileType
{ 
    Missile,
    Laser,
    Bomb
}

public class Projectile : Bullet
{
    public new BulletType BulletType { 
        get => BulletType.Projectile; }

    public ProjectileType ProjectileType;
    public float InitialVelocity;//��ʼ�ٶ�
    public float OffsetAngle;//ƫ�ƽǶ�
    public float Damage;//�˺�
    public float LifeTime;//��������
    public float CoolDown;//����ʱ��
    public bool SelfDamage;//�Ƿ���Լ�����˺�
    public GameObject Prefab;//Ԥ����
    
    ProjectileHandler projectileHandler = null;
    public ProjectileHandler ProjectileHandler
    {
        get { 
            if(projectileHandler)
                return projectileHandler;
            else
                projectileHandler = Prefab.GetComponent<ProjectileHandler>();
            return projectileHandler;
        }
    }
}
