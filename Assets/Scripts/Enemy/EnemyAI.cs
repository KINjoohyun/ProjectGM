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

    [SerializeField]
    private Animator animator;

    private bool isTwoPhase;
    float phaseTwoHealthThreshold;

    private int[] attackPattern1 = new int[] { 1, 2 };
    private int bearAttackPatternIndex = 0;

    private int[] attackPattern2 = new int[] { 1, 2, 3, 2, 3 };
    private int bearAttackPatternIndex2 = 0;

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
        isTwoPhase = false;

        switch (enemyType)
        {
            case EnemyType.Enemy1:
                BTRunner = new BehaviorTreeRunner(BearBT());
                break;

                //case EnemyType.Enemy2:
                //    BTRunner = new BehaviorTreeRunner(Enemy2BT());
                //    break;
                //case EnemyType.Enemy3:
                //    BTRunner = new BehaviorTreeRunner(Enemy3BT());
                //    break;
        }
        originPos = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
            // 3. 

            BTRunner.Operate();
        }

    }

    #region ���� ������ �׼�

    INode BearBT()
    {
        return new SelectorNode
        (
        new List<INode>()
                {
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            // �����߿� �÷��̾ ��Ÿ����� ����� �����Ӵ����� �����ϴ� ������ �߻�

                            new ConditionNode(IsBearPhaseOne), // ������ 1 üũ
                            new ActionNode(() => ExecuteAttackPattern(attackPattern1)) // ������ 1 ���� ����
                        }
                    ),

                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new InverterNode(new ConditionNode(IsBearPhaseOne)),
                            new ActionNode(() => ExecuteAttackPattern(attackPattern2)) // ������ 2 ���� ����
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
        ); ; ;
    }

    #endregion

    #region ���ݳ��

    private bool IsBearPhaseOne()
    {
        if (!isTwoPhase && health <= phaseTwoHealthThreshold)
        {
            isTwoPhase = true;
            Debug.Log("������ 2�� ��ȯ");
            // phaseTwoAttackSequence = 0; // ������ 2�� ���� ������ �ʱ�ȭ
        }
        return !isTwoPhase; // ������ 1�� �ٽ� ��ȯ
    }

    private INode.EnemyState ExecuteAttackPattern(int[] pattern)
    {
        INode.EnemyState result = INode.EnemyState.Failure;

        // ����� ���� ����� ������ �ε��� ����
        int attackSequence = isTwoPhase ? phaseTwoAttackSequence : phaseOneAttackSequence;

        switch (pattern[attackSequence])
        {
            case 1:
                Debug.Log(isTwoPhase ? "������2 ����A" : "������1 ����A");
                result = DoMeleeAttack1();
                break;
            case 2:
                Debug.Log(isTwoPhase ? "������2 ����B" : "������1 ����B");
                result = DoMeleeAttack2();
                break;
            case 3:
                Debug.Log("������2 ����C");
                result = DoMeleeAttack3();
                break;
        }

        if (result == INode.EnemyState.Success)
        {
            if (isTwoPhase)
            {
                phaseTwoAttackSequence = (phaseTwoAttackSequence + 1) % pattern.Length;
            }
            else
            {
                phaseOneAttackSequence = (phaseOneAttackSequence + 1) % pattern.Length;
            }
        }
        return result;
    }

    INode.EnemyState DoMeleeAttack1()
    {
        //if (isAttacking)
        //{
        //    // ���� ���� ���� �÷��̾���� �Ÿ��� üũ�ؼ�
        //    // �÷��̾ ��ġ�� ����� �ٽ� �߰��� �� �ְ�
        //    // ������ �̰� ���� ���̵��� �ö󰡼� �Ⱦ���
        //    if (detectedPlayer == null || Vector3.Distance(detectedPlayer.position, transform.position) >= meleeAttackRange)
        //    {
        //        isAttacking = false;
        //        return INode.EnemyState.Failure;
        //    }
        //    return INode.EnemyState.Success;
        //}

        if (detectedPlayer == null)
        {
            isAttacking = false;
            return INode.EnemyState.Failure;
        }

        if (isAttacking)
            return INode.EnemyState.Failure;

        if (Vector3.Distance(detectedPlayer.position, transform.position) >= meleeAttackRange)
        {
            isAttacking = false;
            return INode.EnemyState.Failure; // �÷��̾ ��Ÿ� ���̸� �߰� ���·� ��ȯ
        }

        if (detectedPlayer != null &&
            Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            isAttacking = true;
            animator.SetTrigger("MeleeAttack_A");
            return INode.EnemyState.Success;
        }

        return INode.EnemyState.Failure;











        //if (isAttacking)
        //    return INode.EnemyState.Failure;

        //if (detectedPlayer != null &&
        //    Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        //{
        //    isAttacking = true;
        //    return INode.EnemyState.Success;
        //}
        //return INode.EnemyState.Failure;
    }

    INode.EnemyState DoMeleeAttack2()
    {
        if (detectedPlayer == null)
        {
            isAttacking = false;
            return INode.EnemyState.Failure;
        }

        if (isAttacking)
            return INode.EnemyState.Failure;

        if (Vector3.Distance(detectedPlayer.position, transform.position) >= meleeAttackRange)
        {
            isAttacking = false;
            return INode.EnemyState.Failure; // �÷��̾ ��Ÿ� ���̸� �߰� ���·� ��ȯ
        }

        if (detectedPlayer != null &&
            Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            isAttacking = true;
            animator.SetTrigger("MeleeAttack_B");
            return INode.EnemyState.Success;
        }

        return INode.EnemyState.Failure;


        //if (isAttacking)
        //    return INode.EnemyState.Failure;

        //if (detectedPlayer != null && Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        //{
        //    isAttacking = true;
        //    return INode.EnemyState.Success;
        //}

        //return INode.EnemyState.Failure;
    }

    INode.EnemyState DoMeleeAttack3()
    {

        if (detectedPlayer == null)
        {
            isAttacking = false;
            return INode.EnemyState.Failure;
        }

        if (isAttacking)
            return INode.EnemyState.Failure;

        if (Vector3.Distance(detectedPlayer.position, transform.position) >= meleeAttackRange)
        {
            isAttacking = false;
            return INode.EnemyState.Failure; // �÷��̾ ��Ÿ� ���̸� �߰� ���·� ��ȯ
        }

        if (detectedPlayer != null &&
            Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            isAttacking = true;
            animator.SetTrigger("MeleeAttack_C");
            return INode.EnemyState.Success;
        }

        return INode.EnemyState.Failure;


        //if (isAttacking)
        //    return INode.EnemyState.Failure;

        //if (detectedPlayer != null && Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        //{
        //    isAttacking = true;
        //    return INode.EnemyState.Success;
        //}
        //return INode.EnemyState.Failure;
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
            animator.SetFloat("MoveSpeed", 0.5f);
            transform.position = Vector3.MoveTowards(transform.position, detectedPlayer.position, Time.deltaTime * movementSpeed);
            transform.LookAt(detectedPlayer); // Look At Player code
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
            animator.SetTrigger("BearWalk");
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
