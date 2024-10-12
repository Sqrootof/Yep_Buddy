using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    public Camera Camera;
    public LineRenderer lineRenderer; // 用于显示激光
    public LayerMask obstacleLayer; // 用于检测的障碍物图层
    public LayerMask enemyLayer; // 用于检测敌人的图层
    public float damageAmount; // 每次激光造成的伤害

    private void Start()
    {
        // 确保 LineRenderer 组件已添加
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.startWidth = 0.1f; // 激光起始宽度
        lineRenderer.endWidth = 0.1f; // 激光结束宽度
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")); // 使用无光照着色器
        lineRenderer.startColor = Color.red; // 激光颜色
        lineRenderer.endColor = Color.red; // 激光颜色
        lineRenderer.positionCount = 3; // 激光有三个点（起点、反射点和终点）

        // 初始隐藏激光
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        // 检测鼠标左键是否按下
        if (Input.GetMouseButton(0)) // 0 表示鼠标左键
        {
            // 获取鼠标位置并更新激光
            Vector3 mouseWorldPosition = GetMousePosition();
            UpdateLaser(mouseWorldPosition);
            lineRenderer.enabled = true; // 显示激光
        }
        else
        {
            lineRenderer.enabled = false; // 隐藏激光
        }
    }

    public Vector3 GetMousePosition()
    {
        // 获取鼠标屏幕位置
        Vector3 mouseScreenPosition = Input.mousePosition;
        float distanceFromCamera = 20f; // 距离相机的深度
        mouseScreenPosition.z = distanceFromCamera;
        Vector3 mouseWorldPosition = Camera.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
    }

    private void UpdateLaser(Vector3 targetPosition)
    {
        // 激光的起始点是武器的位置
        Vector3 startPosition = transform.position;
        Vector3 laserDirection = targetPosition - startPosition;

        // 使用射线检测
        RaycastHit hit;
        float laserMaxDistance = 100f;

        // 初始化激光线段位置
        lineRenderer.SetPosition(0, startPosition); // 激光的起点
        lineRenderer.SetPosition(1, startPosition); // 初始化反射点与起点相同

        // 第一次射线检测
        if (Physics.Raycast(startPosition, laserDirection, out hit, laserMaxDistance, obstacleLayer | enemyLayer))
        {
            // 更新激光的反射点为击中点
            Vector3 firstHitPoint = hit.point;
            lineRenderer.SetPosition(1, firstHitPoint); // 激光的反射点

            // 检测是否击中敌人
            if ((enemyLayer & (1 << hit.collider.gameObject.layer)) != 0)
            {
                EnemyPYPTest enemyHealth = hit.collider.GetComponent<EnemyPYPTest>();
                if (enemyHealth != null)
                {
                    enemyHealth.health -= damageAmount; // 对敌人造成伤害
                }

                // 从敌人击中点继续检测
                Vector3 reflectDirection = laserDirection.normalized; // 使用相同的激光方向
                if (Physics.Raycast(firstHitPoint, reflectDirection, out hit, laserMaxDistance, obstacleLayer))
                {
                    // 更新终点为第二次碰撞点
                    lineRenderer.SetPosition(2, hit.point); // 激光的终点是第二次碰撞点
                }
                else
                {
                    // 没有碰到障碍物，激光达到最大射程
                    lineRenderer.SetPosition(2, firstHitPoint + reflectDirection * (laserMaxDistance - hit.distance)); // 激光的终点
                }
            }
            else
            {
                // 如果碰到障碍物但不是敌人，激光停止
                lineRenderer.SetPosition(2, firstHitPoint); // 激光的终点
            }
        }
        else
        {
            // 如果没有碰到任何障碍物，激光达到最大射程
            lineRenderer.SetPosition(1, startPosition + laserDirection.normalized * laserMaxDistance); // 激光的反射点
            lineRenderer.SetPosition(2, startPosition + laserDirection.normalized * laserMaxDistance); // 激光的终点

            // 检测是否击中敌人
            if (Physics.Raycast(startPosition, laserDirection, out hit, laserMaxDistance, enemyLayer))
            {
                EnemyPYPTest enemyHealth = hit.collider.GetComponent<EnemyPYPTest>();
                if (enemyHealth != null)
                {
                    enemyHealth.health -= damageAmount; // 对敌人造成伤害
                }
            }
        }
    }

    //private void UpdateLaser(Vector3 targetPosition)
    //{
    //    // 激光的起始点是武器的位置
    //    Vector3 startPosition = transform.position;
    //    Vector3 laserDirection = targetPosition - startPosition;

    //    // 使用射线检测
    //    RaycastHit hit;
    //    float laserMaxDistance = 100f;

    //    // 初始化激光线段位置
    //    lineRenderer.SetPosition(0, startPosition); // 激光的起点
    //    lineRenderer.SetPosition(1, startPosition); // 初始化反射点与起点相同

    //    // 第一次射线检测
    //    if (Physics.Raycast(startPosition, laserDirection, out hit, laserMaxDistance, obstacleLayer))
    //    {
    //        // 更新反射点
    //        Vector3 reflectionPoint = startPosition;
    //        lineRenderer.SetPosition(1, reflectionPoint); // 激光的反射点
    //        // 计算反射方向
    //        Vector3 reflectDirection = targetPosition - startPosition;
    //        // 检测是否击中敌人
    //        if ((enemyLayer & (1 << hit.collider.gameObject.layer)) != 0)
    //        {
    //            EnemyPYPTest enemyHealth = hit.collider.GetComponent<EnemyPYPTest>();
    //            if (enemyHealth != null)
    //            {
    //                enemyHealth.health = enemyHealth.health - damageAmount; // 对敌人造成伤害
    //            }
    //        }
    //        else
    //        {
    //            // 更新反射点
    //            reflectionPoint = hit.point;
    //            lineRenderer.SetPosition(1, reflectionPoint); // 激光的反射点
    //                                                          // 计算反射方向
    //            reflectDirection = Vector3.Reflect(laserDirection.normalized, hit.normal);
    //        }

    //        // 第二次射线检测（从反射点开始）
    //        if (Physics.Raycast(reflectionPoint, reflectDirection, out hit, laserMaxDistance, obstacleLayer))
    //        {
    //            // 更新终点为碰撞点
    //            lineRenderer.SetPosition(2, hit.point); // 激光的终点是第二次碰撞点

    //            // 检测是否击中敌人
    //            if ((enemyLayer & (1 << hit.collider.gameObject.layer)) != 0)
    //            {
    //                EnemyPYPTest enemyHealth = hit.collider.GetComponent<EnemyPYPTest>();
    //                if (enemyHealth != null)
    //                {
    //                    enemyHealth.health = enemyHealth.health - damageAmount; // 对敌人造成伤害
    //                }
    //            }
    //        }
    //        else
    //        {
    //            // 如果没有碰到障碍物，激光达到最大射程
    //            lineRenderer.SetPosition(2, reflectionPoint + reflectDirection * laserMaxDistance); // 激光的终点
    //        }
    //    }
    //    else
    //    {
    //        // 如果没有碰到障碍物，激光达到最大射程
    //        lineRenderer.SetPosition(1, startPosition + laserDirection.normalized * laserMaxDistance); // 激光的反射点
    //        lineRenderer.SetPosition(2, startPosition + laserDirection.normalized * laserMaxDistance); // 激光的终点

    //        // 检测是否击中敌人
    //        if (Physics.Raycast(startPosition, laserDirection, out hit, laserMaxDistance, enemyLayer))
    //        {
    //            EnemyPYPTest enemyHealth = hit.collider.GetComponent<EnemyPYPTest>();
    //            if (enemyHealth != null)
    //            {
    //                enemyHealth.health = enemyHealth.health - damageAmount; // 对敌人造成伤害
    //            }
    //        }
    //    }
    //}

    //private void UpdateLaser(Vector3 targetPosition)
    //{
    //    // 激光的起始点是武器的位置
    //    Vector3 startPosition = transform.position;
    //    Vector3 laserDirection = targetPosition - startPosition;

    //    // 使用射线检测
    //    RaycastHit hit;
    //    float laserMaxDistance = 100f;

    //    // 初始化激光线段位置
    //    lineRenderer.SetPosition(0, startPosition); // 激光的起点
    //    lineRenderer.SetPosition(1, startPosition); // 初始化反射点与起点相同

    //    // 第一次射线检测
    //    if (Physics.Raycast(startPosition, laserDirection, out hit, laserMaxDistance, obstacleLayer))
    //    {
    //        // 更新反射点
    //        Vector3 reflectionPoint = hit.point;
    //        lineRenderer.SetPosition(1, reflectionPoint); // 激光的反射点

    //        // 检测是否击中敌人
    //        if ((enemyLayer & (1 << hit.collider.gameObject.layer)) != 0)
    //        {
    //            EnemyPYPTest enemyHealth = hit.collider.GetComponent<EnemyPYPTest>();
    //            if (enemyHealth != null)
    //            {
    //                enemyHealth.health = enemyHealth.health - damageAmount; // 对敌人造成伤害
    //            }
    //        }

    //        // 计算反射方向
    //        Vector3 reflectDirection = Vector3.Reflect(laserDirection.normalized, hit.normal);

    //        // 第二次射线检测（从反射点开始）
    //        if (Physics.Raycast(reflectionPoint, reflectDirection, out hit, laserMaxDistance, obstacleLayer))
    //        {
    //            // 更新终点为碰撞点
    //            lineRenderer.SetPosition(2, hit.point); // 激光的终点是第二次碰撞点

    //            // 检测是否击中敌人
    //            if ((enemyLayer & (1 << hit.collider.gameObject.layer)) != 0)
    //            {
    //                EnemyPYPTest enemyHealth = hit.collider.GetComponent<EnemyPYPTest>();
    //                if (enemyHealth != null)
    //                {
    //                    enemyHealth.health = enemyHealth.health - damageAmount; // 对敌人造成伤害
    //                }
    //            }
    //        }
    //        else
    //        {
    //            // 如果没有碰到障碍物，激光达到最大射程
    //            lineRenderer.SetPosition(2, reflectionPoint + reflectDirection * laserMaxDistance); // 激光的终点
    //        }
    //    }
    //    else
    //    {
    //        // 如果没有碰到障碍物，激光达到最大射程
    //        lineRenderer.SetPosition(1, startPosition + laserDirection.normalized * laserMaxDistance); // 激光的反射点
    //        lineRenderer.SetPosition(2, startPosition + laserDirection.normalized * laserMaxDistance); // 激光的终点

    //        // 检测是否击中敌人
    //        if (Physics.Raycast(startPosition, laserDirection, out hit, laserMaxDistance, enemyLayer))
    //        {
    //            EnemyPYPTest enemyHealth = hit.collider.GetComponent<EnemyPYPTest>();
    //            if (enemyHealth != null)
    //            {
    //                enemyHealth.health = enemyHealth.health - damageAmount; // 对敌人造成伤害
    //            }
    //        }
    //    }
    //}
}
