using UnityEngine;
using UnityEngine.UI;

public class LockerButton : MonoBehaviour
{
    [Header("��� �̹���")]
    [SerializeField]
    private Image lockImage;

    public void LockMode(bool isLock = true)
    {
        lockImage.gameObject.SetActive(isLock);
    }

    private void OnEnable()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        transform.localScale = Vector3.one;
#endif
    }
}
