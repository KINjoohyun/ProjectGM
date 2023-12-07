using UnityEngine;

public class PlayerDeathState : PlayerStateBase
{
    private const string triggerName = "Die";
    public PlayerDeathState(PlayerController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        Debug.Log("�÷��̾� ����");
        controller.player.Animator.SetTrigger(triggerName);
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }
}
