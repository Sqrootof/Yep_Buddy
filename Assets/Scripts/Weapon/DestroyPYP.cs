using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPYP : MonoBehaviour
{
    private float timer = 0f;  // ���ڼ�ʱ
    public float destroyTime = 1f;  // �趨�ݻٵ�ʱ�䣨1�룩

    // Update is called once per frame
    void Update()
    {
        // ���¼�ʱ��
        timer += Time.deltaTime;

        // ����ʱ���Ƿ񳬹��趨ʱ��
        if (timer >= destroyTime)
        {
            // �ݻٵ�ǰ����
            Destroy(gameObject);
        }
    }
}
