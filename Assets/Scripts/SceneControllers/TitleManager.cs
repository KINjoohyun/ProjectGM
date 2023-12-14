using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Notice")]
    public TextMeshProUGUI noticeText;

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

    public void GoGame(string sceneName)
    {
        if (PlayDataManager.curWeapon == null)
        {
            Notice("���⸦ ���� �������ֽʽÿ�.");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }

    private void Notice(string str)
    {
        noticeText.text = str;
        noticeText.gameObject.SetActive(true);
    }

    public void ClearData()
    {
        PlayDataManager.Reset();
        Notice("�����͸� �ʱ�ȭ �Ͽ����ϴ�.");
        moneyText.text = PlayDataManager.data.Gold.ToString();

    }
}
