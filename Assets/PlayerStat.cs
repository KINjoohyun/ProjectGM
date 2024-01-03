using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "PlayerStat")]
public class PlayerStat : Stat
{
    [System.Serializable]
    public class GlobalAnimationSpeed
    {
        [Header("ȸ��")]
        [Range(0.1f, 5)]
        public float evadeSpeed = 1f;
        [Header("����")]
        [Range(0.1f, 5)]
        public float attackSpeed = 1f;
        [Header("Ư������")]
        [Range(0.1f, 5)]
        public float superAttackSpeed = 1f;
        [Header("����")]
        [Range(0.1f, 5)]
        public float sprintSpeed = 1f;
    }

    [Header("�⺻ �ִϸ��̼� �ӵ� ����")]
    [SerializeField]
    public GlobalAnimationSpeed globalSpeed;

    [Header("�¿� ȸ�� �Ÿ�")]
    public float leftRightEvadeDistance = 2f;

    [Header("���� ȸ�� �Ÿ�")]
    public float frontBackEvadeDistance = 2f;

    [Header("���� �Ÿ�")]
    public float moveDistance = 2f;

    [Header("ȸ�� ���� �ð�(sec)")]
    public float evadeTime;

    [Header("�׷α� ���ݿ� �ʿ��� ȸ�� ����Ʈ")]
    public float maxEvadePoint;
    
    [Header("ȸ�� �� ȸ�� ����Ʈ")]
    [Range(-100, 100)]
    public float evadePoint;

    [Header("ȸ�� �� �ǰ� ����� ����")]
    [Range(0f, 3f)]
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

    [Header("���� �ӵ�")]
    [Range(0.1f, 5f)]
    public float attackSpeed = 1f;

    [Header("Ư�� ���� ����")]
    public float superAttackRate = 3f;

    [Header("�ǰݽ� ����� ��� Ȯ��")]
    [Range(0f, 1f)]
    public float blockRate = 0f;

    public override Attack CreateAttack(LivingObject attacker, LivingObject defender, bool groggy)
    {
        var player = attacker as Player;
        var enemyStat = defender.stat as EnemyStat;
        float damage = player.Stat.AttackDamage + player.CurrentWeapon.attack;

        if (enemyStat.weaknessType == player.CurrentWeapon.type)
        {
            damage += (damage * (player.CurrentWeapon.weakDamage));
        }

        if (player.GetComponent<PlayerController>().CurrentState == PlayerController.State.SuperAttack)
        {
            damage *= superAttackRate;
        }

        var critical = Random.value < player.Stat.Critical;
        if (critical)
        {
            damage *= player.Stat.CriticalDamage;
        }

        if (enemyStat != null)
        {
            damage -= enemyStat.Defence;
            if (damage < 0)
            {
                damage = 0;
            }
        }
        return new Attack((int)damage, critical, groggy);
    }

    public override string ToString()
    {
        return base.ToString() + $"\nevadeTime: {evadeTime}\tmaxEvadePoint: {maxEvadePoint}\tevadePoint: {evadePoint}\t\nevadeDamageRate: {evadeDamageRate}\tjustEvadeTime: {justEvadeTime}\tjustEvadePoint: {justEvadePoint}\t\nhitEvadePoint: {hitEvadePoint}\tgroggyTime: {groggyTime}\thitInvincibleTime: {hitInvincibleTime}t";
    }
}
