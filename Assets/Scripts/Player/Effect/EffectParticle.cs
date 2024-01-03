using UnityEngine;

public class EffectParticleSystem : EffectBase
{
    private ParticleSystem particle;
    [Header("���� ����")]
    public Vector3 directionOffset;
    [Header("��ġ ����")]
    public Vector3 positionOffset;
    private Transform targetTransform;

    protected override void Update()
    {
        //Not Use base.Update
        return;
    }
    public override void Init(Transform playerTransform = null)
    {
        targetTransform = playerTransform;
        particle = GetComponent<ParticleSystem>();
    }

    public override void PlayStart(Vector3 direction = default)
    {
        base.PlayStart();
        particle.transform.position = targetTransform.position;

        var rotation = targetTransform.rotation;
        var correctedRotation = Quaternion.Euler(rotation.eulerAngles + directionOffset);

        if (direction != default)
        {
            var normalizedDirection = direction.normalized;
            var finalRotation = Quaternion.LookRotation(normalizedDirection) * correctedRotation;
            particle.transform.rotation = finalRotation;
            particle.transform.localPosition += new Vector3(
                positionOffset.x * normalizedDirection.x,
                positionOffset.y,
                positionOffset.z * normalizedDirection.z
            );
        }
        else
        {
            particle.transform.localPosition += positionOffset;
            particle.transform.rotation = correctedRotation;
        }
        particle.Play();
    }

    public override void PlayEnd()
    {
        particle.Stop();
        base.PlayEnd();
    }
}
