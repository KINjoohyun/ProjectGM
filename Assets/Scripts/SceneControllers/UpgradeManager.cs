using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("���� ��� ��ư��")]
    public List<CurrentItemButton> equipButtons;

    [Header("�÷��̾� ����")]
    public UpgradeInfoPanel upgradeInfoPanel;

    [Header("��ư ������")]
    public UpgradeEquipButton prefab;

    private void Awake()
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        
    }

    private void Start()
    {
        foreach (var renewal in equipButtons)
        {
            renewal.Renewal();
        }
        upgradeInfoPanel.Renewal();

        ShowWeapons(true);
    }

    public void ShowWeapons(bool isOn)
    {
        if (!isOn)
        {
            return;
        }
        Clear();
        Debug.Log("W");
    }

    public void ShowArmors(bool isOn)
    {
        if (!isOn)
        {
            return;
        }
        Clear();
        Debug.Log("A");

    }

    public void Clear()
    {
        Debug.Log("Clear");
    }
}
