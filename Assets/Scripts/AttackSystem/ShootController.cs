using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Pool;

public class ShootController : MonoBehaviour
{
    #region"������"
    [SerializeField]List<Bullet> Bullets = new();//��ǰ���ص��ӵ�

    [SerializeField]List<Projectile> CurrentProjectileBlock = new();//��ǰ�ӵ���
    [SerializeField]List<Gain> CurrentGainsBlock = new();//��ǰ�����
    float Cooldown = 0;//��ȴʱ��
    float LastShootTime = -1;//�ϴ����ʱ��
    [SerializeField]int BlockHeadIndex = 0;//��ǰ�ӵ�����ʼλ��
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (Time.time - LastShootTime >= Cooldown) {
                Shoot();
            } 
        }
    }

    public void Shoot()
    { 
        //��ȡ����һ���ӵ���
        GetNextBulletBlock();
        Cooldown = 0;
        foreach (var Projectile in CurrentProjectileBlock)
        {
            Projectile newData = Instantiate(Projectile);
            Cooldown += newData.CoolDown;
            foreach (var gain in CurrentGainsBlock)
            {
                gain.DeployGain(newData);
            }
            GameObject newbullet = Instantiate(Projectile.Prefab);
            newbullet.GetComponent<ProjectileHandler>().SetProjectileData(Projectile);
        }
    }

    /// <summary>
    /// ��ȡ����һ���ӵ���
    /// </summary>
    /// <returns></returns>
    void  GetNextBulletBlock()
    {
        int Index = BlockHeadIndex++;
        if(BlockHeadIndex == Bullets.Count) 
            BlockHeadIndex = 0;

        CurrentProjectileBlock.Clear();
        CurrentGainsBlock.Clear();
        int stepcount = 1;
        Bullet CurrentBullet;
        while (stepcount > 0)
        {
            stepcount--;
            CurrentBullet = Bullets[Index++];
            if (Index == Bullets.Count)
                Index = 0;
            if (CurrentProjectileBlock.Contains(CurrentBullet as Projectile) || CurrentGainsBlock.Contains(CurrentBullet as Gain)){
                break;
            }
            switch (CurrentBullet.BulletType)
            {
                case BulletType.Extend:
                    stepcount += (CurrentBullet as Extend).StepExtension;
                    break;

                case BulletType.Gain:
                    stepcount++;
                    CurrentGainsBlock.Add(CurrentBullet as Gain);
                    break;

                case BulletType.Projectile:
                    CurrentProjectileBlock.Add(CurrentBullet as Projectile);
                    break;

                default:
                    Debug.LogError("Undefined Bullet Type");
                    break;
            }
        }
    }
}
