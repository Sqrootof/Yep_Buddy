using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于处理射弹的公共生命周期
public class ProjectileHandler : MonoBehaviour
{
    protected Projectile ProjectileData;

    float AwakeTime;

    public delegate IEnumerator ProjectileLifeEvent();
    public ProjectileLifeEvent OnPeojectileAwake;
    public ProjectileLifeEvent OnProjectileSleep;
    public ProjectileLifeEvent OnProjectileHit;
    public ProjectileLifeEvent OnProjectileFly;
    // Start is called before the first frame update
    void Start()
    {
        AwakeTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - AwakeTime >= ProjectileData.LifeTime)
            DestroyProjectile();
                        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void DestroyProjectile()
    {
        if (OnProjectileSleep != null)
            StartCoroutine(OnProjectileSleep());

        Destroy(gameObject,0.15f);
    }

    public void SetProjectileData(Projectile projectile){ 
        ProjectileData = projectile;
    }

    public ProjectileHandler(Projectile projectile){ 
        ProjectileData = projectile;
    }
}
