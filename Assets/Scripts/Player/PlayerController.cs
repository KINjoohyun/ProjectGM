using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player { get; private set; }
    private TouchManager touchManager;

    public enum State
    {
        Idle,
        Attack,
        SuperAttack,
        Evade,
        Sprint,
        Hit,
        Death
    }
    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();
    public State CurrentState { get; private set; }
    public State nextState = State.Idle;

    #region Weapon
    public enum WeaponPosition
    {
        Hand, Wing
    }
    public WeaponPosition currentWeaponPosition { get; private set; }
    private Weapon equipWeapon = null;
    public Transform leftHand;
    public Transform rightHand;
    public Transform leftWing;
    public Transform rightWing;
    public WeaponSO weaponSO;
    #endregion

    #region IK
    private Transform subHandle;
    #endregion

    private void Awake()
    {
        player = GetComponent<Player>();
        touchManager = TouchManager.Instance;

        // equip weapon test
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }
        equipWeapon = PlayDataManager.curWeapon;

        foreach (var armor in PlayDataManager.curArmor)
        {
            if (armor.Value != null)
            {
                var table = CsvTableMgr.GetTable<ArmorTable>().dataTable;
                player.Stat.Defence += table[armor.Value.id].defence;
            }
        }

        StateInit();
    }

    private void Start()
    {
        player.CurrentWeapon = weaponSO.MakeWeapon(equipWeapon, rightHand, player.Animator);
        if (equipWeapon.weaponType == WeaponType.Tonpa)
        {
            player.FakeWeapon = Instantiate(player.CurrentWeapon);
        }
        subHandle = player.CurrentWeapon.transform.Find("LeftHandle");

        //MoveWeaponPosition(WeaponPosition.Wing);
        
        touchManager.SwipeListeners += OnSwipe;
        touchManager.HoldListeners += OnHold;
        touchManager.HoldEndListeners += HoldEnd;
    }

    private void OnDestroy()
    {
        touchManager.SwipeListeners -= OnSwipe;
        touchManager.HoldListeners -= OnHold;
        touchManager.HoldEndListeners -= HoldEnd;
    }

    private void Update()
    {
        if (player.Enemy == null)
        {
            return;
        }
        if (CurrentState == State.Death)
        {
            return;
        }

        stateManager?.Update();
        Vector3 relativePos = player.Enemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);

        if (player.IsGroggy)
        {
            SetState(State.Hit);
        }

        #region EvadePoint
        if (player.evadePoint >= player.Stat.maxEvadePoint)
        {
            player.GroggyAttack = true;
        }
        if (player.GroggyAttack)
        {
            player.evadePoint -= Time.deltaTime * (player.Stat.maxEvadePoint / player.Stat.groggyTime);
            if (player.evadePoint <= 0f)
            {
                player.evadePoint = 0f;
                player.GroggyAttack = false;
            }
        }
        #endregion

        #region Test Input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.evadePoint += 50;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.Stat.AttackDamage = (player.Stat.AttackDamage == 0) ? 70 : 0;
            Debug.Log(player.Stat.AttackDamage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.Stat.Defence = (player.Stat.Defence == 0) ? -100 : 0;
            Debug.Log(player.Stat.Defence);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (player.FakeWeapon == null)
            {
                player.FakeWeapon = Instantiate(player.CurrentWeapon);
                MoveWeaponPosition(WeaponPosition.Wing);
            }
            else
            {
                Destroy(player.FakeWeapon.gameObject);
                player.FakeWeapon = null;
            }
        }
        #endregion
    }

    private void FixedUpdate()
    {
        stateManager?.FixedUpdate();
    }

    #region Touch Event
    private void OnSwipe()
    {
        if (CurrentState == State.Hit || CurrentState == State.Death)
        {
            nextState = State.Evade;
            return;
        }

        SetState(State.Evade);
    }
    private void OnHold()
    {
        if (CurrentState != State.Idle)
        {
            nextState = State.Sprint;
            return;
        }

        SetState(State.Sprint);
    }
    private void HoldEnd()
    {

    }
    #endregion

    #region Animation Event
    private void BeforeAttack()
    {
        player.attackState = Player.AttackState.Before;
        nextState = State.Idle;
    }
    private void Attack()
    {
        player.attackState = Player.AttackState.Attack;
        if (player.CurrentWeapon == null)
        {
            return;
        }

        if (player.DistanceToEnemy < player.CurrentWeapon.attackRange)
        {
            ExecuteAttack(player, player.Enemy);
            if (player.GroggyAttack)
            {
                player.GroggyAttack = false;
                player.evadePoint = 0f;
            }
        }
        player.attackState = Player.AttackState.AfterStart;
        nextState = State.Idle;
    }
    private void AfterAttack()
    {
        player.attackState = Player.AttackState.AfterEnd;
    }
    private void EndAttack()
    {
        player.attackState = Player.AttackState.End;
    }
    private void EndAnimationDefault()
    {
        SetState(State.Idle);
    }
    #endregion

    public void SetState(State newState)
    {
        if (newState == CurrentState)
        {
            return;
        }
#if UNITY_EDITOR
        //Debug.Log($"--------- ChangeState: {newState} ---------");
#endif
        CurrentState = newState;
        stateManager?.ChangeState(states[(int)newState]);
    }

    private void StateInit()
    {
        states.Add(new PlayerIdleState(this));
        states.Add(new PlayerAttackState2(this));
        states.Add(new PlayerSuperAttackState(this));
        states.Add(new PlayerEvadeState(this));
        states.Add(new PlayerSprintState(this));
        states.Add(new PlayerHitState(this));
        states.Add(new PlayerDeathState(this));

        SetState(State.Idle);
    }

    private void ExecuteAttack(LivingObject attacker, LivingObject defender)
    {
        Attack attack = player.Stat.CreateAttack(attacker, defender, player.GroggyAttack);
        var attackables = defender.GetComponents<IAttackable>();

        Handheld.Vibrate();

        //List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Right, devices);

        //foreach (var device in devices)
        //{
        //    UnityEngine.XR.HapticCapabilities capabilities;
        //    if (device.TryGetHapticCapabilities(out capabilities))
        //    {
        //        if (capabilities.supportsImpulse)
        //        {
        //            uint channel = 0;
        //            float amplitude = 0.5f;
        //            float duration = 1.0f;
        //            device.SendHapticImpulse(channel, amplitude, duration);
        //        }
        //    }
        //}

        foreach (var attackable in attackables)
        {
            attackable.OnAttack(player.gameObject, attack);
        }

    }

    public void MoveWeaponPosition(WeaponPosition position)
    {
        return;
        //currentWeaponPosition = position;
        //switch (position)
        //{
        //    case WeaponPosition.Hand:
        //        player.CurrentWeapon.transform.SetParent(rightHand, false);
        //        player.FakeWeapon?.transform.SetParent(leftHand, false);
        //        break;
        //    case WeaponPosition.Wing:
        //        player.CurrentWeapon.transform.SetParent(rightWing, false);
        //        player.FakeWeapon?.transform.SetParent(leftWing, false);
        //        break;
        //}
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (subHandle == null || currentWeaponPosition != WeaponPosition.Hand)
        {
            return;
        }
        player.Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        player.Animator.SetIKPosition(AvatarIKGoal.LeftHand, subHandle.transform.position);
    }
}