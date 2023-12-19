using TMPro;
using UnityEngine;

public class TitleManager : MonoBehaviour, IRenewal
{
    [Header("������ �ؽ�Ʈ")]
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        Renewal();
    }

    public void ClearData()
    {
        PlayDataManager.Reset();
        MyNotice.Instance.Notice("�����͸� �ʱ�ȭ �Ͽ����ϴ�.");
        moneyText.text = PlayDataManager.data.Gold.ToString();

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.Renewal();
        }
    }

    public void Renewal()
    {
        moneyText.text = PlayDataManager.data.Gold.ToString();
    }
}
