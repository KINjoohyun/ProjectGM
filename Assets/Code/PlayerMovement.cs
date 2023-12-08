
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerMovement : MonoBehaviour
    {
    public float speed = 5f;

    private void Update()
    {
        // WASD Ű �Է��� �޾� �̵� ���͸� ����մϴ�.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // �÷��̾��� ���� ��ġ
        Vector3 currentPosition = transform.position;

        // ���ο� ��ġ ���
        Vector3 newPosition = currentPosition + movement;

        // �̵� �� ���� ������� ���ϵ��� ó��
        RaycastHit hit;
        if (Physics.Raycast(currentPosition, movement.normalized, out hit, movement.magnitude))
        {
            // �浹�� �߻��ϸ� �̵��� ����
            newPosition = hit.point;
        }

        // ���ο� ��ġ�� �̵�
        transform.position = newPosition;
    }
}


