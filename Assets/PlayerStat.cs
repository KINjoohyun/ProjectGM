using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "PlayerStat")]
public class PlayerStat : Stat
{
    [Header("회피 판정 시간(sec)")]
    public float evadeTime;

    [Header("그로기 공격에 필요한 회피 포인트")]
    public float maxEvadePoint;
    
    [Header("회피 시 회피 포인트")]
    [Range(-100, 100)]
    public float evadePoint;

    [Header("회피 시 대미지 배율")]
    [Range(0f, 3f)]
    public float evadeDamageRate;

    [Header("저스트 회피 판정 시간(sec)")]
    public float justEvadeTime;

    [Header("저스트 회피 시 회피 포인트")]
    [Range(-100, 100)] 
    public float justEvadePoint;

    [Header("피격 시 회피 포인트")]
    [Range(-100, 100)]
    public float hitEvadePoint;

    [Header("그로기 유발 시간(sec)")]
    public float groggyTime;

    [Header("피격 시 무적 시간(sec)")]
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
            if (damage < 0)
            {
                damage = 0;
            }
        }
        return new Attack((int)damage, critical, groggy);
    }
}
