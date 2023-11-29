using UnityEngine;

[CreateAssetMenu(menuName = "Stat", fileName = "DefaultStat")]
public class Stat : ScriptableObject
{
    [Header("�ʱ� ü��")]
    public int HP;

    [Header("���ݷ�")]
    public int AttackDamage;

    [Header("����")]
    public int Defence;

    [Header("�̵��ӵ�")]
    public float MoveSpeed;

    [Header("ġ��Ÿ Ȯ��")]
    public float Critical;

    [Header("ġ��Ÿ ����")]
    public float CriticalDamage;

    public virtual Attack CreateAttack(LivingObject attacker, LivingObject defender, bool groogy = false)
    {
        float damage = attacker.stat.AttackDamage;

        var critical = Random.value < attacker.stat.Critical;
        if (critical)
        {
            damage *= attacker.stat.CriticalDamage;
        }

        if (defender != null)
        {
            damage -= defender.stat.Defence;
        }

        return new Attack((int)damage, critical, groogy);
    }
}