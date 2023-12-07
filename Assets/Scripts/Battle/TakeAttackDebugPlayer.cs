using TMPro;
using UnityEngine;

public class TakeAttackDebugPlayer : MonoBehaviour, IAttackable
{
    private Player player;

    private GameObject hpUI;
    private GameObject evadeUI;
    private GameObject evadePointUI;
    private GameObject groggyAttackUI;
    private GameObject hitUI;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        if (TestLogManager.Instance == null)
        {
            return;
        }
        hpUI = TestLogManager.Instance.MakeUI();
        hpUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"{player.HP}";
        hpUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "�÷��̾�HP";

        evadeUI = TestLogManager.Instance.MakeUI(EvadeAction);
        evadeUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "ȸ�� ����";
        evadePointUI = TestLogManager.Instance.MakeUI();
        evadePointUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "ȸ�� ����Ʈ\n�׷α� ����";

        groggyAttackUI = TestLogManager.Instance.MakeUI(GroggyAction);
        groggyAttackUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Ư������";

        hitUI = TestLogManager.Instance.MakeUI(HitAction);
        hitUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "�ǰ� ����";
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        HPAction();
    }

    #region TestLog Events
    private void HPAction()
    {
        hpUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = player.HP switch
        {
            var x when x >= player.Stat.HP * 0.4f && x < player.Stat.HP * 0.7f => $"<color=yellow>{player.HP}</color>/{player.Stat.HP}",
            var x when x < player.Stat.HP * 0.4f => $"<color=red>{player.HP}</color>/{player.Stat.HP}",
            _ => $"<color=green>{player.HP}</color>/{player.Stat.HP}"
        };
    }

    private void EvadeAction()
    {
        evadeUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = player.evadeTimer switch
        {
            float x when (player.GetComponent<PlayerController>().currentState == PlayerController.State.Evade && x < player.Stat.justEvadeTime) => "<color=green>����Ʈ ȸ��</color>",
            float x when (player.GetComponent<PlayerController>().currentState == PlayerController.State.Evade && x >= player.Stat.justEvadeTime && x < player.Stat.evadeTime) => "<color=yellow>�Ϲ� ȸ��</color>",
            _ => "<color=red>���</color>"
        };
        evadePointUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = player.GroggyAttack switch
        {
            true => $"<color=yellow>{(int)player.evadePoint}</color>/{player.Stat.maxEvadePoint}\n<color=green>�׷α� ���� ON</color>",
            false => $"<color=blue>{(int)player.evadePoint}</color>/{player.Stat.maxEvadePoint}\n<color=white>�׷α� ���� OFF</color>"
        };
    }
    private void GroggyAction()
    {
        groggyAttackUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = player.Enemy.IsGroggy switch
        {
            true => $"<color=green>ON</color>",
            false => $"<color=white>OFF</color>"
        };
    }
    private void HitAction()
    {
        hitUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = player.GetComponent<PlayerController>().currentState switch
        {
            PlayerController.State.Hit => $"<color=red>ON</color>",
            _ => $"<color=white>OFF</color>"
        };
    }
    #endregion
}
