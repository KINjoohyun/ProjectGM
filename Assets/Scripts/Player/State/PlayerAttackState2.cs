using UnityEngine;

public class PlayerAttackState2 : PlayerStateBase
{
    private Animator animator;
    private const string triggerName = "Attack";
    private float comboTimer = 0f;
    private float comboTime = 0.5f;

    public PlayerAttackState2(PlayerController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        comboTimer = 0f;
        animator = controller.player.Animator;

        controller.MoveWeaponPosition(PlayerController.WeaponPosition.Hand);

        animator.SetTrigger(triggerName);
    }

    public override void Update()
    {
        if (controller.player.Enemy.IsGroggy == true)
        {
            controller.SetState(PlayerController.State.SuperAttack);
        }

        switch (controller.player.attackState)
        {
            case Player.AttackState.Before:
                //����
                //ȸ�� ����
                break;
            case Player.AttackState.Attack:
                //��������
                //ȸ�� �Ұ�
                break;
            case Player.AttackState.AfterStart:
                //�ĵ� ����
                //�ൿ �Ұ���
                //���Է� ����
                if (TouchManager.Instance.Holded)
                {
                    animator.SetTrigger(triggerName);
                }
                break;
            case Player.AttackState.AfterEnd:
                //�ĵ� ����
                //ȸ�� ����
                if (TouchManager.Instance.Holded)
                {
                    animator.SetTrigger(triggerName);
                }
                break;
            case Player.AttackState.End:
                //�ִϸ��̼� ����
                if (!animator.GetBool(triggerName))
                {
                    comboTimer += Time.deltaTime;
                    Debug.Log(comboTimer);
                    if (comboTimer >= comboTime)
                    {
                        controller.SetState(PlayerController.State.Idle);
                    }
                }
                break;
        }

        Debug.Log(controller.player.attackState.ToString());
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        controller.player.Animator.ResetTrigger(triggerName);
    }
}
