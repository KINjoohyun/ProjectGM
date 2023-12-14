using CsvHelper;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [System.Serializable]
    private class EffectInfo
    {
        [Header("����Ʈ ����")]
        public EffectType effectType;
        [Header("����Ʈ ������")]
        public ParticleSystem effectPrefab;
        [HideInInspector]
        public ParticleSystem effect = null;
        [Header("���� ����")]
        public Vector3 directionOffset;
        [Header("��ġ ����")]
        public Vector3 positionOffset;

    }

    [Header("����Ʈ ���")]
    [SerializeField]
    private List<EffectInfo> effectInfos = null;

    private void Awake()
    {
        foreach(var info in effectInfos)
        {
            info.effect = Instantiate(info.effectPrefab);
            info.effect.transform.SetParent(transform);
            info.effect.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
    }

    public void PlayEffect(EffectType type, Vector3 direction = default)
    {
        var effectInfo = effectInfos.Find((x) =>
        {
            return x.effectType == type;
        });

        if (effectInfo == null)
        {
            Debug.Log($"��ϵ��� ���� ����Ʈ: {type}");
            return;
        }

        var effect = effectInfo.effect;
        effect.transform.position = transform.position;

        var rotation = transform.rotation;
        var correctedRotation = Quaternion.Euler(rotation.eulerAngles + effectInfo.directionOffset);

        if (direction != default)
        {
            var normalizedDirection = direction.normalized;
            var finalRotation = Quaternion.LookRotation(normalizedDirection) * correctedRotation;
            effect.transform.rotation = finalRotation;
            effect.transform.localPosition += new Vector3(
                effectInfo.positionOffset.x * normalizedDirection.x,
                effectInfo.positionOffset.y,
                effectInfo.positionOffset.z * normalizedDirection.z
            );
        }
        else
        {
            effect.transform.localPosition += effectInfo.positionOffset;
            effect.transform.rotation = correctedRotation;
        }


        effect.Play();
    }
}
