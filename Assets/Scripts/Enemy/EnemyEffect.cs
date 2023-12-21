using UnityEngine;
using static EnemyAI;

public class EnemyEffect : MonoBehaviour
{
    [Header("A ���� ����Ʈ")]
    public GameObject EffectTypeA;

    [Header("B ���� ����Ʈ")]
    public GameObject EffectTypeB;

    [Header("C ���� ����Ʈ")]
    public GameObject EffectTypeC;

    [Header("D ���� ����Ʈ")]
    public GameObject EffectTypeD;

    [Header("Range A ���� ����Ʈ")]
    public GameObject EffectTypeRA;

    [Header("Range B ���� ����Ʈ")]
    public GameObject EffectTypeRB;

    [Header("Range C ���� ����Ʈ")]
    public GameObject EffectTypeRC;
    Vector3 offset;

    private EnemyAI enemyAi;

    private void Start()
    {
        enemyAi = GetComponent<EnemyAI>();
    }


    void EffectEnemyType(string pattern)
    {
        offset = Vector3.zero;

        switch (pattern)
        {
            case "A":

                switch (enemyAi.enemyType)
                {
                    case EnemyType.Bear:
                        offset += transform.forward * 1f + transform.up * 1f;
                    break;

                    case EnemyType.Alien:
                        offset = Vector3.zero;
                        break;

                    case EnemyType.Boar:
                        offset += transform.forward * 2f + transform.up * 1f;
                        break;

                    case EnemyType.Wolf:
                        offset += transform.forward * 4.5f + transform.up * 0.1f;
                        break;
                }
                break;

            case "B":

                switch (enemyAi.enemyType)
                {
                    case EnemyType.Bear:
                        offset += transform.forward * 7.0f + transform.up * 1f;
                        break;

                    case EnemyType.Alien:
                        offset += transform.forward * 2f + transform.up * 1f;
                        break;

                    case EnemyType.Boar:
                        offset += transform.forward * 3f + transform.up * 1f;
                        break;

                    case EnemyType.Wolf:
                        offset += transform.forward * 2f + transform.up * 1f;
                        break;

                    case EnemyType.Spider:

                        // 1. ���ν��Ͻ� ����Ʈ �޾ƿ���
                        // 2. �� ��ġ�� �����ϱ�
                        // 3. ���ð� �޾ƿ���
                        // 4. ���ð���ŭ �ִϸ��̼� ���ӽð� �ø���


                        //offset += transform.forward * 2f + transform.up * 1f;
                        break;
                        //case EnemyType.Spider:
                        //    offset += transform.forward * 3.5f + transform.up * 1f;
                        //    break;
                }
                break;

            case "C":

                switch (enemyAi.enemyType)
                {
                    case EnemyType.Bear:
                        offset += transform.forward * 2.5f + transform.up * 3f;
                        break;

                    case EnemyType.Alien:
                        offset = Vector3.zero;
                        break;

                    case EnemyType.Boar:
                        offset += transform.forward * 5f + transform.up * 1f;
                        break;
                }
                break;

            case "RA":

                switch (enemyAi.enemyType)
                {
                    case EnemyType.Bear:
                        offset += transform.forward * 1.5f + transform.up * 2.5f;
                        break;
                }
                break;

        }
    }

    public void AttackEffectA()
    {
        EffectEnemyType("A");

        if (EffectTypeA != null)
        {
            GameObject effectInstance = Instantiate(EffectTypeA, transform.position + offset, transform.rotation);

            DestroyEffect(effectInstance);
        }
    }

    public void AttackEffectB()
    {
        EffectEnemyType("B");

        if (EffectTypeB != null)
        {
            GameObject effectInstance = Instantiate(EffectTypeB, transform.position + offset, transform.rotation);

            DestroyEffect(effectInstance);
        }
    }

    // �̰� �� �ٲ�ߵ�?

    public void AttackEffectC()
    {
        EffectEnemyType("C");

        if (EffectTypeC != null)
        {
            GameObject effectInstance = Instantiate(EffectTypeC, transform.position + offset, transform.rotation);

            DestroyEffect(effectInstance);
        }
    }

    public void AttackEffectD()
    {
        EffectEnemyType("D");

        if (EffectTypeD != null)
        {
            GameObject effectInstance = Instantiate(EffectTypeD, transform.position + offset, transform.rotation);

            DestroyEffect(effectInstance);
        }
    }

    public void AttackEffectRA()
    {
        EffectEnemyType("RA");

        if (EffectTypeRA != null)
        {
            GameObject effectInstance = Instantiate(EffectTypeRA, transform.position + offset, transform.rotation);
            Rigidbody rb = effectInstance.GetComponent<Rigidbody>();
            Vector3 forceDirection = (enemyAi.detectedPlayer.position - transform.position).normalized; // �÷��̾� �������� ���� ���մϴ�.
            float forceMagnitude = 10f;
            rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);

            effectInstance.GetComponent<EnemyProjectile>().enemyAi = enemyAi;

            DestroyEffect(effectInstance);
        }
    }


    public void DestroyEffect(GameObject instance)
    {
        ParticleSystem particleSystem = instance.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Destroy(instance, particleSystem.main.duration);
        }
        else
        {
            Destroy(instance, 1.5f);
        }
    }
}
