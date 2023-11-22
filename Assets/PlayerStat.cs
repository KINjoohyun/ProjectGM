using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "PlayerStat")]
public class PlayerStat : Stat
{
    [Header("ȸ�� ���� �ð�")]
    public float evadeTime;

    [Header("����Ʈ ȸ�� ���� �ð�")]
    public float evadeJustTime;

    [Header("�ǰ� �� ���� �ð�")]
    public float hitInvincibleTime;
}
