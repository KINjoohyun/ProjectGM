using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class TakeAttackAndEvade : MonoBehaviour, IAttackable
{
    private enum EvadeSuccesss
    {
        None, Normal, Just
    }
    private EvadeSuccesss evade;
    private Player player;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (player == null)
        {
            return;
        }

        evade = player.evadeTimer switch
        {
            float x when (x < player.Stat.justEvadeTime) => EvadeSuccesss.Just,
            float x when (x >= player.Stat.justEvadeTime && x < player.Stat.evadeTime) => EvadeSuccesss.Normal,
            _ => EvadeSuccesss.None
        };

        player.evadePoint += evade switch
        {
            EvadeSuccesss.Just => player.Stat.justEvadePoint,
            EvadeSuccesss.Normal => player.Stat.evadePoint,
            _ => player.Stat.hitEvadePoint
        };

        player.evadePoint = Mathf.Clamp(player.evadePoint, 0, player.Stat.maxEvadePoint);

        switch (evade)
        {
            case EvadeSuccesss.None:
                Debug.Log("피격");
                player.HP -= attack.Damage;
                player.IsGroggy = attack.IsGroggy;
                break;
            case EvadeSuccesss.Normal:
                Debug.Log("일반 회피");
                player.HP -= (int)(attack.Damage * player.Stat.evadeDamageRate);
                break;
            case EvadeSuccesss.Just:
                Debug.Log("저스트 회피");
                break;
        }

        if (player.HP <= 0)
        {
            player.HP = 0;
            var destructables = player.GetComponents<IDestructable>();
            foreach (var destructable in destructables)
            {
                destructable.OnDestruction(attacker);
            }
        }
    }
}
