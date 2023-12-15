using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [Header("Notice")]
    [SerializeField]
    private TextMeshProUGUI noticeText;

    [Header("BLACK")]
    [SerializeField]
    private FadeEffects BLACK;

    private void Awake()
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }
    }

    public void GoGame(string sceneName)
    {
        if (PlayDataManager.curWeapon == null)
        {
            Notice("���⸦ ���� �������ֽʽÿ�.");
            return;
        }
        BLACK.FadeOut(sceneName);
    }

    private void Notice(string str)
    {
        noticeText.text = str;
        noticeText.gameObject.SetActive(true);
    }
}
