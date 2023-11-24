using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "PlayerStat")]
public class PlayerStat : Stat
{
    [Header("ȸ�� ���� �ð�(sec)")]
    public float evadeTime;
    
    [Header("ȸ�� �� ȸ�� ����Ʈ")]
    [Range(-100, 100)]
    public int evadePoint;

    [Header("ȸ�� �� ����� ���� ����")]
    [Range(0f, 1f)]
    public float evadeDamageRate;

    [Header("����Ʈ ȸ�� ���� �ð�(sec)")]
    public float justEvadeTime;

    [Header("����Ʈ ȸ�� �� ȸ�� ����Ʈ")]
    [Range(-100, 100)]
    public int justEvadePoint;

    [Header("�ǰ� �� ȸ�� ����Ʈ")]
    [Range(-100, 100)]
    public int hitEvadePoint;

    [Header("�ǰ� �� ���� �ð�(sec)")]
    public float hitInvincibleTime;
}
