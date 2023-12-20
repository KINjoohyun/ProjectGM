using UnityEngine;

public class TakeAttack : MonoBehaviour, IAttackable
{
    private LivingObject attackTarget;

    [Header("���� �ǰ� ����Ʈ")]
    public GameObject hitEffectPrefab;

    private void Awake()
    {
        attackTarget = GetComponent<LivingObject>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (attackTarget == null || attacker == null)
        {
            return;
        }

        attackTarget.HP -= attack.Damage;
        attackTarget.IsGroggy = attack.IsGroggy; // Ʈ�� �޽� �Ҵ��ϴ� �κ�

        CreateHitEffect(transform.position);

        if (attackTarget.HP <= 0)
        {
            attackTarget.HP = 0;
            var destructables = attackTarget.GetComponents<IDestructable>();
            foreach(var destructable in destructables)
            {
                destructable.OnDestruction(attacker);
            }
        }
    }
    private void CreateHitEffect(Vector3 position)
    {
        var hitEffect = Instantiate(hitEffectPrefab, position, Quaternion.identity);

        ParticleSystem particleSystem = hitEffect.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Destroy(hitEffect, particleSystem.main.duration);
        }
        else
        {
            Destroy(hitEffect, 1.5f);
        }
    }
}
