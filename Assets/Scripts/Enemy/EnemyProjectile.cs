//using UnityEngine;

//public class EnemyProjectile : EnemyAI
//{
//    public float damage = 2f;

//    // EnemyAI enemyAi;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Player player = other.GetComponent<Player>();
//            if (player != null)
//            {
//                // ��üũ, �ʱ�ȭ Ȯ��
//                Debug.Log(gameObject.GetComponent<EnemyAI>());
//                Debug.Log(player);
//                Debug.Log(damage);

//                // ������ �޴� �Լ��� �������ؼ�
//                ExecuteAttack(gameObject.GetComponent<EnemyAI>(), player, damage);

//                //player.TakeDamage(damage);
//            }

//            Debug.Log(gameObject);
//            Destroy(gameObject);
//        }
//    }
//}
