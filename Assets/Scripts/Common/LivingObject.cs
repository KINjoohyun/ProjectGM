using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingObject : MonoBehaviour
{
    [Header("Stat ����")]
    public Stat statLink;
    [HideInInspector]
    public Stat stat; //Copy

    public int HP { get; set; }
    public bool IsGroggy { get; set; }
    [Header("��� �� �̺�Ʈ")]
    public UnityEvent OnDeathEvent;

    protected virtual void Awake()
    {
        stat = Instantiate(statLink);
        HP = stat.HP;
        IsGroggy = false;

        if (OnDeathEvent.GetPersistentEventCount() == 0)
        {
            UnityAction<LivingObject> unityAction = GameManager.instance.GameOver;
            UnityEventTools.AddObjectPersistentListener(OnDeathEvent, unityAction, this);
        }
    }
}