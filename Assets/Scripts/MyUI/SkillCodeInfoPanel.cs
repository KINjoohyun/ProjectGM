using TMPro;
using UnityEngine;

public class SkillCodeInfoPanel : MonoBehaviour
{
    [Header("�̸� �ؽ�Ʈ")]
    public TextMeshProUGUI nameText;

    [Header("���� �ؽ�Ʈ")]
    public TextMeshProUGUI levelText;

    private void OnEnable()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        transform.localScale = Vector3.one;
#endif
    }
}
