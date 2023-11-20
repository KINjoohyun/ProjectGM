using UnityEngine;

[CreateAssetMenu(menuName = "Stat", fileName = "DefaultStat")]
public class Stat : ScriptableObject
{
    [Header("ü��")]
    public float HP;

    [Header("���ݷ�")]
    public float AttackDamage;

    [Header("����")]
    public float Defence;

    [Header("�̵��ӵ�")]
    public float MoveSpeed;

    [Header("ġ��Ÿ Ȯ��")]
    public float Critical;

    [Header("ġ��Ÿ ����")]
    public float CriticalDamage;

}