using UnityEngine;


public abstract class Effect : MonoBehaviour
{
    [Header("����Ʈ ���� �ð�")]
    [Tooltip("��ƼŬ �ý����� ����")]
    [SerializeField]
    private float duration;
    [Header("����Ʈ ������Ʈ")]
    public GameObject prefab;
    //[Header("����Ʈ ���� ���� ����")]
    //[Tooltip("���� �ð��� �����ϰ� �ൿ�� ������ ����Ʈ�� �����")]
    //public bool useForceStop = false; // �����ȳ�...

    private float timer = 0f;
    private bool isPlay = false;

    private void Awake()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        if (!isPlay)
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
        gameObject.SetActive(isPlay = false);
    }

    public virtual void PlayStart(Vector3 direction = default)
    {
        timer = 0f;
        gameObject.SetActive(isPlay = true);
    }

    public abstract void Init(Transform targetTransform = null);
}
