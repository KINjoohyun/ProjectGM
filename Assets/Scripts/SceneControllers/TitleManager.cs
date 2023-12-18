using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [Header("������ �ؽ�Ʈ")]
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        moneyText.text = PlayDataManager.data.Gold.ToString();
    }

    public void ClearData()
    {
        PlayDataManager.Reset();
        MyNotice.Instance.Notice("�����͸� �ʱ�ȭ �Ͽ����ϴ�.");
        moneyText.text = PlayDataManager.data.Gold.ToString();

    }
}
