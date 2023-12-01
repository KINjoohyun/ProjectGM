// AttackCell.cs
using UnityEngine;

public class AttackCell : MonoBehaviour
{
    public bool playerInside = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("��Ʈ���ſ���");

            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("��Ʈ����Exit");

            playerInside = false;
        }
    }
}
