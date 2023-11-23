using System;

public sealed class ActionNode : INode
{
    Func<INode.EnemyState> onUpdate = null;

    public ActionNode(Func<INode.EnemyState> onUpdate)
    {
        this.onUpdate = onUpdate;
    }

    public INode.EnemyState Evaluate() => onUpdate?.Invoke() ?? INode.EnemyState.Failure; // ���� ��� �ϴ� ���ڸ��� ���ư���
}