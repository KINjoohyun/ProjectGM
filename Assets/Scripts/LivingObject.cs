using UnityEngine;

public abstract class LivingObject : MonoBehaviour
{
    [Header("Stat ����")]
    public Stat stat;
    public int HP { get; set; }

    protected virtual void Awake()
    {
        HP = stat.HP;
    }
}