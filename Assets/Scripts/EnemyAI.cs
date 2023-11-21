using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
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

    Vector3 originPos;

    private bool IsMeleeAttack1 = false;
    private bool IsMeleeAttack2 = false;
    private bool IsMeleeAttack3 = false;

    BehaviorTreeRunner BTRunner;
    Transform detectedPlayer;

    [SerializeField]
    private EnemyType enemyType;

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
            case EnemyType.Enemy2:
                BTRunner = new BehaviorTreeRunner(Enemy2BT());
                break;
            case EnemyType.Enemy3:
                BTRunner = new BehaviorTreeRunner(Enemy3BT());
                break;
        }
        originPos = transform.position;
    }



    private void Update()
    {
        BTRunner.Operate();
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
                            //new ActionNode(CheckEnemyWithinMeleeAttackRange),
                            new ActionNode(DoMeleeAttack1),
                            new ActionNode(DoMeleeAttack2),
                            new ActionNode(DoMeleeAttack3),
                            //new ActionNode(DoMeleeAttack2)
                            //new ActionNode(DoMeleeAttack2)
                            //new ActionNode(DoMeleeAttack2)
                            //new ActionNode(DoMeleeAttack2)
                            //new ActionNode(DoMeleeAttack2)
                            //new ActionNode(DoMeleeAttack2)
                            //new ActionNode(DoMeleeAttack2)

                            /// �̰Ÿ� �б⸦ �� ���� ���� �ֳ�?
                            /// ��� ������ �ؾ��ϳ�?
                            /// 1�� ���� �������̸� �翬�� �ٸ������� �����ϸ�ȵ�
                            /// 1�������� ������ 8�������� ������ �׷���
                            /// 1�� ������ 2�ʾ�
                            /// �÷��̾� ��ġ�� �׵��� �ٲ��
                            /// ���� ������ �÷��̾ ��û �ִٰ� ġ��
                            /// 10�������� ��ħ �� �÷��̾ �����ϴ� �����̾�
                            /// �׷��� ���ʹ� 10���������� ���ߵ�
                            /// 

                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckDetectEnemy),
                            new ActionNode(MoveToDetectEnemy),
                        }
                    ),
                    new ActionNode(MoveToOriginPosition)
                }
        );
    }

    INode Enemy2BT()
    {
        return new SelectorNode
        (
        new List<INode>()
                {
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(DoRangeAttack1),
                            new ActionNode(DoRangeAttack2),
                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckDetectEnemy),
                            new ActionNode(MoveToDetectEnemy),
                        }
                    ),
                    new ActionNode(MoveToOriginPosition)
                }
        );
    }

    INode Enemy3BT()
    {
        return new SelectorNode
        (
        new List<INode>()
                {
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(DoMeleeAttack1),
                            new ActionNode(DoMeleeAttack2),
                            new ActionNode(DoRangeAttack1),
                            new ActionNode(DoRangeAttack2),
                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckDetectEnemy),
                            new ActionNode(MoveToDetectEnemy),
                        }
                    ),
                    new ActionNode(MoveToOriginPosition)
                }
        );
    }

    #endregion

    #region ���ݳ��

    //INode.EnemyState CheckPlayerWithMell

    INode.EnemyState CheckEnemyWithinMeleeAttackRange()
    {
        if (detectedPlayer != null && Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            Debug.Log("Attack ����");
            return INode.EnemyState.Attack;
        }

        return INode.EnemyState.Fail;
    }



    INode.EnemyState DoMeleeAttack1()
    {

        if (detectedPlayer != null &&
            Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            Debug.Log("���� ���� 111!");
            return INode.EnemyState.Attack;
        }

        return INode.EnemyState.Fail;
    }

    INode.EnemyState DoMeleeAttack2()
    {
        if (detectedPlayer != null && Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            Debug.Log("���� ���� 222!");
            return INode.EnemyState.Attack;
        }
        return INode.EnemyState.Fail;
    }

    INode.EnemyState DoMeleeAttack3()
    {
        if (detectedPlayer != null && Vector3.Distance(detectedPlayer.position, transform.position) < meleeAttackRange)
        {
            Debug.Log("���� ���� 333!");
            return INode.EnemyState.Attack;
        }
        return INode.EnemyState.Fail;
    }


    INode.EnemyState DoRangeAttack1()
    {
        if (detectedPlayer != null)
        {
            Debug.Log("���Ÿ� ���� 111!");
            return INode.EnemyState.Attack;
        }

        return INode.EnemyState.Fail;
    }

    INode.EnemyState DoRangeAttack2()
    {
        if (detectedPlayer != null)
        {
            Debug.Log("���Ÿ� ���� 222!");
            return INode.EnemyState.Attack;
        }

        return INode.EnemyState.Fail;
    }

    #endregion

    #region ���� �� �̵� ���
    INode.EnemyState CheckDetectEnemy()
    {
        var overlapColliders = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Player"));

        if (overlapColliders.Length > 0)
        {
            detectedPlayer = overlapColliders[0].transform;
            return INode.EnemyState.Attack;
        }

        detectedPlayer = null;
        return INode.EnemyState.Fail;
    }

    INode.EnemyState MoveToDetectEnemy()
    {
        if (detectedPlayer != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, detectedPlayer.position, Time.deltaTime * movementSpeed);
            return INode.EnemyState.Trace;
        }

        return INode.EnemyState.Fail;
    }
    #endregion

    #region  ���ڸ� ���ư��� ���߿� �ٸ������� ����
    INode.EnemyState MoveToOriginPosition()
    {
        if (Vector3.Distance(originPos, transform.position) < 0.01f)
        {
            return INode.EnemyState.Attack;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, Time.deltaTime * movementSpeed);
            return INode.EnemyState.Trace;
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

    //INode SettingBT()
    //{
    //    return new SelectorNode
    //        (
    //            new List<INode>()
    //            {
    //                new SequenceNode
    //                (
    //                    new List<INode>()
    //                    {
    //                        new ActionNode(CheckEnemyWithinMeleeAttackRange),
    //                        new ActionNode(DoMeleeAttack),
    //                    }
    //                ),
    //                new SequenceNode
    //                (
    //                    new List<INode>()
    //                    {
    //                        new ActionNode(CheckDetectEnemy),
    //                        new ActionNode(MoveToDetectEnemy),
    //                    }
    //                ),
    //                new ActionNode(MoveToOriginPosition)
    //            }
    //        );
    //}


}
