using UnityEngine;

public class Vampire : Skill
{
    private float duration = 1.0f;
    private float timer = 0.0f;

    public Vampire(int id, int level)
        : base(id, level)
    {

    }

    private void Start()
    {
        Init();

        if (level < 3)
        {
            gameObject.SetActive(false);
            Debug.Log(gameObject.name + " OFF");
            return;
        }
        // Player Drain On


    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = 0.0f;
            player.HP -= Mathf.RoundToInt(player.stat.HP / 100);
        }
        // Player Die => Death
        // Player Alive => player.HP = Mathf.Clamp(player.HP -= Mathf.RoundToInt(player.stat.HP / 100), 1, player.HP);
    }
}
