using UnityEngine;

public class TakeAttack : MonoBehaviour, IAttackable
{
    private LivingObject attackTarget;

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
}
