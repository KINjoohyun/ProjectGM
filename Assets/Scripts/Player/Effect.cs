using UnityEngine;


public abstract class Effect : MonoBehaviour
{
    [Header("����Ʈ ���� �ð�")]
    [Tooltip("��ƼŬ �ý����� ����")]
    [SerializeField]
    private float duration;
    [Header("������")]
    public GameObject prefab;

    private float timer = 0f;
    private bool isPlay = false;

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

    protected virtual void PlayEnd()
    {
        gameObject.SetActive(isPlay = false);
    }

    public virtual void PlayStart(Vector3 direction = default)
    {
        timer = 0f;
        gameObject.SetActive(isPlay = true);
    }

    public abstract void Init(Transform transform = null);
}
