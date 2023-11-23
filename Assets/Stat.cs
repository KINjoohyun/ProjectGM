using UnityEngine;

[CreateAssetMenu(menuName = "Stat", fileName = "DefaultStat")]
public class Stat : ScriptableObject
{
    [Header("ü��")]
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

}