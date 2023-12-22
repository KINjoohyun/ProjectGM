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
}
