using Newtonsoft.Json.Serialization;
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

        var damage = attack.Damage;
        damage -= player.Stat.Defence;

        EvadeCheck();
        switch (evade)
        {
            case EvadeSuccesss.None:
                //player.effects.PlayEffect(EffectType.Hit);
                player.IsGroggy = attack.IsGroggy;
                break;
            case EvadeSuccesss.Normal:
                //player.effects.PlayEffect(EffectType.Hit);
                damage = (int)(damage * player.Stat.evadeDamageRate);
                break;
            case EvadeSuccesss.Just:
                player.effects.PlayEffect(PlayerEffectType.JustEvade);
                damage = 0;
                break;
        }
        if (damage <= 0)
        {
            damage = 0;
        }
        player.HP -= damage;

        if (player.HP <= 0)
        {
            player.HP = 0;
            player.GetComponent<PlayerController>().SetState(PlayerController.State.Death);
            var destructables = player.GetComponents<IDestructable>();
            foreach (var destructable in destructables)
            {
                destructable.OnDestruction(attacker);
            }
        }
    }
    
    private void EvadeCheck()
    {
        if (player.GetComponent<PlayerController>().CurrentState != PlayerController.State.Evade)
        {
            evade = EvadeSuccesss.None;
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
    }
}
