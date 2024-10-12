using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    public Camera Camera;
    public LineRenderer lineRenderer; // 用于显示激光
    public LayerMask obstacleLayer; // 用于检测的障碍物图层

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
        if (Physics.Raycast(startPosition, laserDirection, out hit, laserMaxDistance, obstacleLayer))
        {
            // 更新反射点
            Vector3 reflectionPoint = hit.point;
            lineRenderer.SetPosition(1, reflectionPoint); // 激光的反射点

            // 计算反射方向
            Vector3 reflectDirection = Vector3.Reflect(laserDirection.normalized, hit.normal);

            // 第二次射线检测（从反射点开始）
            if (Physics.Raycast(reflectionPoint, reflectDirection, out hit, laserMaxDistance, obstacleLayer))
            {
                // 更新终点为碰撞点
                lineRenderer.SetPosition(2, hit.point); // 激光的终点是第二次碰撞点
            }
            else
            {
                // 如果没有碰到障碍物，激光达到最大射程
                lineRenderer.SetPosition(2, reflectionPoint + reflectDirection * laserMaxDistance); // 激光的终点
            }
        }
        else
        {
            // 如果没有碰到障碍物，激光达到最大射程
            lineRenderer.SetPosition(1, startPosition + laserDirection.normalized * laserMaxDistance); // 激光的反射点
            lineRenderer.SetPosition(2, startPosition + laserDirection.normalized * laserMaxDistance); // 激光的终点
        }
    }
}
