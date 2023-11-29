using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "PlayerStat")]
public class PlayerStat : Stat
{
    [Header("ȸ�� ���� �ð�(sec)")]
    public float evadeTime;

    [Header("�׷α� ���ݿ� �ʿ��� ȸ�� ����Ʈ")]
    public float maxEvadePoint;
    
    [Header("ȸ�� �� ȸ�� ����Ʈ")]
    [Range(-100, 100)]
    public float evadePoint;

    [Header("ȸ�� �� ����� ���� ����")]
    [Range(0f, 1f)]
    public float evadeDamageRate;

    [Header("����Ʈ ȸ�� ���� �ð�(sec)")]
    public float justEvadeTime;

    [Header("����Ʈ ȸ�� �� ȸ�� ����Ʈ")]
    [Range(-100, 100)] 
    public float justEvadePoint;

    [Header("�ǰ� �� ȸ�� ����Ʈ")]
    [Range(-100, 100)]
    public float hitEvadePoint;

    [Header("�׷α� ���� �ð�(sec)")]
    public float groggyTime;

    [Header("�ǰ� �� ���� �ð�(sec)")]
    public float hitInvincibleTime;

    public override Attack CreateAttack(LivingObject attacker, LivingObject defender, bool groggy)
    {
        Player player = attacker as Player;
        float damage = attacker.stat.AttackDamage;
        damage += player.CurrentWeapon.attack;

        var critical = Random.value < attacker.stat.Critical;
        if (critical)
        {
            damage *= attacker.stat.CriticalDamage;
        }

        if (defender != null)
        {
            damage -= defender.stat.Defence;
        }
        return new Attack((int)damage, critical, groggy);
    }
}
