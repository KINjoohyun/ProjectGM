using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColliderInfo : MonoBehaviour
{
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        // ĸ�� �ݶ��̴��� ������
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (capsuleCollider == null)
        {
            Debug.LogError("Capsule Collider not found on the object.");
        }

        // �ݶ��̴� ���� ���
        DisplayColliderInfo();
    }

    private void DisplayColliderInfo()
    {
        if (capsuleCollider != null)
        {
            Debug.Log("Capsule Collider Info:");
            Debug.Log($"Height: {capsuleCollider.height}");
            Debug.Log($"Radius: {capsuleCollider.radius}");
            Debug.Log($"Center: {capsuleCollider.center}");
        }
    }
}


