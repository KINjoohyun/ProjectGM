using TMPro;
using UnityEngine;
using static Player;

public class PlayerTestLog : MonoBehaviour
{
    private Player player;

    private GameObject hpUI;
    private GameObject evadeUI;
    private GameObject evadePointUI;
    private GameObject groggyAttackUI;
    private GameObject stateUI;
    private GameObject attackUI;

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
        hpUI = TestLogManager.Instance.MakeUI(HPAction);
        hpUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "�÷��̾�HP";

        evadeUI = TestLogManager.Instance.MakeUI(EvadeAction);
        evadeUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "ȸ��";

        evadePointUI = TestLogManager.Instance.MakeUI();
        evadePointUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "ȸ�� ����Ʈ\n�׷α� ����";

        groggyAttackUI = TestLogManager.Instance.MakeUI(GroggyAction);
        groggyAttackUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Ư������";

        stateUI = TestLogManager.Instance.MakeUI(StateAction);
        stateUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "����";

        attackUI = TestLogManager.Instance.MakeUI(AttackAction);
        attackUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "���� ��";
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
    private void StateAction()
    {
        stateUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = player.GetComponent<PlayerController>().currentState switch
        {
            PlayerController.State.Death => $"<color=red>����</color>",
            PlayerController.State.Hit => $"<color=red>�ǰ�</color>",
            PlayerController.State.Attack => $"<color=yellow>����==>>></color>",
            PlayerController.State.Evade => $"<color=green>ȸ��</color>",
            PlayerController.State.Sprint => $"<color=orange>����</color>",
            PlayerController.State.SuperAttack => $"<color=blue>Ư������</color>",
            PlayerController.State.Idle => $"<color=white>�Ϲ�</color>",
            _ => $"<color=white>�Ϲ�</color>"
        };
    }

    private void AttackAction()
    {
        if (player.GetComponent<PlayerController>().currentState != PlayerController.State.Attack)
        {
            attackUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"<color=white>���� �ƴ�</color>";
            return;
        }
        attackUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = player.attackState switch
        {
            AttackState.Before => $"<color=yellow>��������</color>\n<color=green>ȸ�� ����</color>",
            AttackState.Attack => $"<color=yellow>����</color>\n<color=red>ȸ�� �Ұ�</color>",
            AttackState.AfterStart => $"<color=yellow>�ĵ����� ����</color>\n<color=red>ȸ�� �Ұ�</color>",
            AttackState.AfterEnd => $"<color=yellow>�ĵ����� ����</color>\n<color=green>ȸ�� ����</color>",
            AttackState.End => $"<color=yellow>���� ����</color>\n<color=green>ȸ�� ����</color>",
            _ => $"<color=yellow></color>"
        };
    }
    #endregion
}
