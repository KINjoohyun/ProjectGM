using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ���� ������ bool������ �ϳ������Ѵ�
/// �� �ൿ ���ϸ��� ���ݽð��� �����Ѵ�
/// ���ݽð��� ������ ���� ���� �ൿ�� �Ѵ�. ��, ������ �ٽ� �ϰų� Ʈ���̽� ���¿� �����Ѵ�
/// ������ ü���� 0���ϰ� �Ǹ� ��� �ൿ�� ���߰� �׾���Ѵ�.
/// </summary>

public class EnemyAI : MonoBehaviour
{
    [Header("Range")]
    [SerializeField]
    float detectRange = 10f;
    [SerializeField]
    float meleeAttackRange = 5f;

    [Header("Movement")]
    [SerializeField]
    float movementSpeed = 10f;

    [SerializeField]
    float attackPower = 1f;

    [SerializeField]
    float health = 100f;

    float phaseTwoHealthThreshold = 50f;

    bool isTwoPhase = false;

    private int phaseOneAttackSequence = 0;
    private int phaseTwoAttackSequence = 0;

    Vector3 originPos;

    BehaviorTreeRunner BTRunner;
    Transform detectedPlayer;

    [SerializeField]
    private EnemyType enemyType;

    private bool isAttacking = false;
    private float attackDuration = 2f;
    private float attackTimer = 0f;

    public enum EnemyType
    {
        Enemy1,
        Enemy2,
        Enemy3,
        Enemy4,
        Enemy5,
        Enemy6,
        Enemy7,
        Enemy8,
        Enemy9,
        Enemy10,
        Enemy11,
        Enemy12,
        Enemy13,
        Enemy14,
        Enemy15,
        Enemy16,
        Enemy17,
        Enemy18,
        Enemy19,
        Enemy20,
    }

    private void Awake()
    {
        switch (enemyType)
        {
            case EnemyType.Enemy1:
                BTRunner = new BehaviorTreeRunner(Enemy1BT());
                break;

                //case EnemyType.Enemy2:
                //    BTRunner = new BehaviorTreeRunner(Enemy2BT());
                //    break;
                //case EnemyType.Enemy3:
                //    BTRunner = new BehaviorTreeRunner(Enemy3BT());
                //    break;
        }
        originPos = transform.position;
    }

    private void Update()
    {
        if (health <= 0)
            return;

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration)
            {
                isAttacking = false;
                attackTimer = 0f;
            }
        }
        if (!isAttacking)
        {
            // �������� 2���� �߻��ϴ� ����
            // 1. ���۷���Ʈ ȣ�� �ֱ� ����
            // 2. bool���� ���� �߰� ������?

            BTRunner.Operate();
        }

    }

    #region ���� ������ �׼�

    INode Enemy1BT()
    {
        return new SelectorNode
        (
        new List<INode>()
                {
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ConditionNode(IsAttackSequenceOne),
                            new ActionNode(DoMeleeAttack1), // �׳� 1���ݸ� ��
                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ConditionNode(IsAttackSequenceTwo),
                            new ActionNode(DoMeleeAttack2),
                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(DetectPlayer),
                            new ActionNode(TracePlayer),
                        }
                    ),
                    new ActionNode(MoveToOriginPosition)
                }
        );
    }

    #endregion

    #region ���ݳ��

    //INode Enemy1AttackPattern()
    //{
    //    if (isTwoPhase)
    //    {
    //        return PhaseTwoAttackPattern();
    //    }
    //    else
    //    {
    //        return PhaseOneAttackPattern();
    //    }
    //}

    //// 1������ ���� ����
    //INode PhaseOneAttackPattern()
    //{
        

    //    Debug.Log(phaseOneAttackSequence);

    //    switch (phaseOneAttackSequence)
    //    {
            
    //        case 0:
    //            phaseOneAttackSequence = 1;
    //            return new ActionNode(DoMeleeAttack1);
                
    //        case 1:
    //            phaseOneAttackSequence = 0;
    //            return new ActionNode(DoMeleeAttack2);

    //        default:
    //            return null;
    //    }
    //}

    //// 2������ ���� ����
    //INode PhaseTwoAttackPattern()
    //{
    //    phaseTwoAttackSequence = (phaseTwoAttackSequence + 1) % 5;

    //    switch (phaseTwoAttackSequence)
    //    {
    //        case 0:
    //        case 3:
    //            return new ActionNode(DoMeleeAttack1);
    //        case 1:
    //        case 4:
    //            return new ActionNode(DoMeleeAttack2);
    //        case 2:
    //            return new ActionNode(DoMeleeAttack3);
    //        default:
    //            return null;
    //    }
    //}

    private bool IsAttackSequenceOne()
    {
        return phaseOneAttackSequence == 0;
    }

    private bool IsAttackSequenceTwo()
    {
        return phaseOneAttackSequence == 1;
    }
    /// <summary>
    /// //////
    /// </summary>
    /// <returns></returns>
    INode.EnemyState DoMeleeAttack1()
    {
        if (isAttacking)
            return INode.EnemyState.Failure;

        if (detectedPlayer != null &&
            Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            Debug.Log("���� ���� 111!");
            isAttacking = true;
            phaseOneAttackSequence = 1;
            return INode.EnemyState.Success;
        }

        return INode.EnemyState.Failure;
    }

    INode.EnemyState DoMeleeAttack2()
    {
        if (isAttacking)
            return INode.EnemyState.Failure;

        if (detectedPlayer != null && Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            Debug.Log("���� ���� 222!");
            isAttacking = true;
            phaseOneAttackSequence = 0;
            return INode.EnemyState.Success;
        }
        return INode.EnemyState.Failure;
    }

    INode.EnemyState DoMeleeAttack3()
    {
        if (isAttacking)
            return INode.EnemyState.Failure;

        if (detectedPlayer != null && Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            Debug.Log("���� ���� 333!");
            return INode.EnemyState.Success;
        }
        return INode.EnemyState.Failure;
    }
    #endregion

    #region ���� �� �̵� ���
    INode.EnemyState DetectPlayer()
    {
        var overlapColliders = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Player"));

        if (overlapColliders.Length > 0)
        {
            detectedPlayer = overlapColliders[0].transform;
            return INode.EnemyState.Success;
        }

        detectedPlayer = null;
        return INode.EnemyState.Failure;
    }

    INode.EnemyState TracePlayer()
    {
        if (detectedPlayer != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, detectedPlayer.position, Time.deltaTime * movementSpeed);
            return INode.EnemyState.Running;
        }

        return INode.EnemyState.Failure;
    }
    #endregion

    #region  ���ڸ� ���ư��� ���߿� �ٸ������� ����
    INode.EnemyState MoveToOriginPosition()
    {
        if (Vector3.Distance(originPos, transform.position) < 0.01f)
        {
            return INode.EnemyState.Success;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, Time.deltaTime * movementSpeed);
            return INode.EnemyState.Running;
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);
    }
}
