using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour, IRenewal
{
    [Header("������ �ؽ�Ʈ")]
    public TextMeshProUGUI moneyText;

    [Header("�������")]
    [SerializeField]
    private Toggle vibeToggle;

    public static TitleManager Instance;

    private void Awake()
    {
        Instance = this;

        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }
        vibeToggle.isOn = PlayDataManager.data.Vibration;

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

    public void OnVibe(bool value)
    {
        PlayDataManager.data.Vibration = value;
        PlayDataManager.Save();
    }
}
