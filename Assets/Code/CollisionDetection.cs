using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // �浹�� ��ü�� "Player" �±׸� ���� ��� ���⼭ ���ϴ� ������ �����մϴ�.
                Debug.Log("Player collided with transparent wall!");
            }
        }
    }

