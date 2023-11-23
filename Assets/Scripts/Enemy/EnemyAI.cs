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

    private bool isTwoPhase = false;
    float phaseTwoHealthThreshold;

    private int bearAttackPatternIndex = 0;
    private int[] attackPattern = new int[] { 1, 2, 3, 2, 3 };


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
    }

    private void Awake()
    {
        phaseTwoHealthThreshold = health * 0.5f;

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
        // �׽�Ʈ
        if (Input.GetKeyDown(KeyCode.H))
        {
            health /= 2;
            Debug.Log("���Ƿ� ����, ���� ü�� : " + health);
        }

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
                        new List<INode>() // 2������
                        {
                            //new ConditionNode(IsBearPhaseTwo),
                            //new ActionNode(EnterBearPhaseTwo),

                            // ������ ���̴� ����
                            // ����ġ�� ����
                            // 

                            new ConditionNode(IsAttackSequenceOne),
                            new ActionNode(DoMeleeAttack1),
                            new ConditionNode(IsAttackSequenceTwo),
                            new ActionNode(DoMeleeAttack2),
                            new ConditionNode(IsAttackSequenceTwo),
                            new ActionNode(DoMeleeAttack3),
                             new ConditionNode(IsAttackSequenceTwo),
                            new ActionNode(DoMeleeAttack2),
                            new ConditionNode(IsAttackSequenceTwo),
                            new ActionNode(DoMeleeAttack3),
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


    private bool IsAttackSequenceOne()
    {
        return phaseOneAttackSequence == 0;
    }

    private bool IsAttackSequenceTwo()
    {
        return phaseOneAttackSequence == 1;
    }

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

    INode.EnemyState EnterBearPhaseTwo()
    {
        // 2������ ���� ���Ͽ� ���� �ൿ ����
        // ������ �ε����� �����ε�?

        INode.EnemyState result = INode.EnemyState.Failure; // ���Ϸ� ���·� �ʱ�ȭ
        switch (attackPattern[bearAttackPatternIndex])
        {
            case 1:
                Debug.Log("��¥ 2������ ���� ����ġ�� ����");
                result = DoMeleeAttack1();
                break;
            case 2:
                result = DoMeleeAttack2();
                break;
            case 3:
                result = DoMeleeAttack3();
                break;
        }
        
        if (result == INode.EnemyState.Success) // ������ �����Ҷ��� �ε��� ������Ʈ
        {
            bearAttackPatternIndex = (bearAttackPatternIndex + 1) % attackPattern.Length;
        }

        return result;
    }

    private bool IsBearPhaseTwo()
    {
        // 2������ ���� ���� �ѹ��� �����ϵ��� ����
        if (!isTwoPhase && health <= phaseTwoHealthThreshold)
        {
            isTwoPhase = true; // 2������ ���� ��ȯ
            Debug.Log("������2 ����!!!!");
            return true;
        }
        return false;
    }
}
