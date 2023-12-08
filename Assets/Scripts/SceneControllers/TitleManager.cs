using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI noticeText;

    public void GoGame(string sceneName)
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }
        
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
}
