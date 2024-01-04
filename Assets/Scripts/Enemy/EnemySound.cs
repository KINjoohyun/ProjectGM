using UnityEngine;
using System.Collections.Generic;

public class EnemySound : MonoBehaviour
{
    // ����
    public AudioClip attackClip1;
    public AudioClip attackClip2;
    public AudioClip attackClip3;
    public AudioClip attackClip4;

    // ��ȿ (����)
    public AudioClip bearRoarClip;
    public AudioClip wolfRoarClip;
    public AudioClip spiderRoarClip;
    public AudioClip AlienRoarClip;
    public AudioClip boarRoarClip;

    // �׷α�, ����
    public AudioClip bearDieClip;
    public AudioClip wolfDieClip;
    public AudioClip spiderDieClip;
    public AudioClip AlienDieClip;
    public AudioClip boarDieClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        List<AudioClip> attackClips = new List<AudioClip> { attackClip1, attackClip2, attackClip3, attackClip4 };

        audioSource.clip = attackClips[Random.Range(0, attackClips.Count)];
        audioSource.Play();
    }

    public void PlayGrogyAndDieSound(string enemyType)
    {
        switch (enemyType)
        {
            case "Bear":
                audioSource.clip = bearDieClip;
                break;
            case "Wolf":
                audioSource.clip = wolfDieClip;
                break;
            case "Spider":
                audioSource.clip = spiderDieClip;
                break;
            case "Alien":
                audioSource.clip = AlienDieClip;
                break;
            case "Boar":
                audioSource.clip = boarDieClip;
                break;
            default:
                return;
        }
        audioSource.Play();
    }

    public void PlayRoarSound(string enemyType)
    {
        switch (enemyType)
        {
            case "Bear":
                audioSource.clip = bearRoarClip;
                break;
            case "Wolf":
                audioSource.clip = wolfRoarClip;
                break;
            case "Spider":
                audioSource.clip = spiderRoarClip;
                break;
            case "Alien":
                audioSource.clip = AlienRoarClip;
                break;
            case "Boar":
                audioSource.clip = boarRoarClip;
                break;
            default:
                return;
        }
        audioSource.Play();
    }
}

