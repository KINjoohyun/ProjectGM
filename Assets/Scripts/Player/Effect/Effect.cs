using UnityEngine;


public abstract class Effect : MonoBehaviour
{
    [Header("����Ʈ ���� �ð�")]
    [Tooltip("��ƼŬ �ý����� ����")]
    [SerializeField]
    public float duration;
    //[Header("����Ʈ ������Ʈ")]
    protected GameObject prefab;
    //[Header("����Ʈ ���� ���� ����")]
    //[Tooltip("���� �ð��� �����ϰ� �ൿ�� ������ ����Ʈ�� �����")]
    //public bool useForceStop = false; // �����ȳ�...

    private float timer = 0f;
    public bool IsPlay { get; protected set; } = false;

    protected virtual void Awake()
    {
        prefab = gameObject;
    }

    protected virtual void Update()
    {
        if (!IsPlay)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer > duration)
        {
            PlayEnd();
        }
    }

    public virtual void PlayEnd()
    {
        gameObject.SetActive(IsPlay = false);
    }

    public virtual void PlayStart(Vector3 direction = default)
    {
        timer = 0f;
        gameObject.SetActive(IsPlay = true);
    }

    public abstract void Init(Transform targetTransform = null);
}
