using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPYPTest : MonoBehaviour
{
    public float moveSpeed = 5f; // ����ƶ��ٶ�
    public float jumpForce = 5f; // ��Ծ����
    private Rigidbody rb; // 3D �������
    private bool isGrounded; // �ж�����Ƿ��ڵ���

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // ��ȡ�������
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // �����ƶ�����
        Jump(); // ������Ծ����
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal"); // ��ȡˮƽ����
        Vector3 move = new Vector3(moveInput, 0, 0); // �����ƶ�����
        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, rb.velocity.z); // ���¸����ٶ�
    }

    private void Jump()
    {
        // ���ո���Ƿ��£���������ڵ�����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // �����һ�����ϵ���
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �����������ײ
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // ����ڵ�����
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �뿪����ʱ����Ϊ���ڵ���
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // ��Ҳ��ڵ�����
        }
    }
}
