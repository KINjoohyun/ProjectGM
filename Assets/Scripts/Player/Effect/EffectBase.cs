using UnityEngine;


public abstract class EffectBase : MonoBehaviour
{
    [Header("����Ʈ ���� �ð�")]
    [Tooltip("��ƼŬ �ý����� ����")]
    [SerializeField]
    public float duration;

    public float Timer { get; private set; }
    public bool IsPlay { get; protected set; } = false;

    protected virtual void Update()
    {
        if (!IsPlay)
        {
            return;
        }
        Timer += Time.deltaTime;
        if (Timer > duration)
        {
            PlayEnd();
        }
    }

    public virtual void PlayStart(Vector3 direction = default)
    {
        Timer = 0f;
        gameObject.SetActive(IsPlay = true);
    }
    public virtual void PlayEnd()
    {
        gameObject.SetActive(IsPlay = false);
    }

    public abstract void Init(Transform targetTransform);
}
