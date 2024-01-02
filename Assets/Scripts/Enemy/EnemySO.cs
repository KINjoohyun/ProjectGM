using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemySO")]
public class EnemySO : ScriptableObject
{
    [System.Serializable]
    public class EnemyData
    {
        [Header("���� ID")]
        public int ID;
        [Header("���� ������")]
        public GameObject prefab;
    }

    public EnemyData[] datas;

    public GameObject MakeEnemy(int monsterID, Transform tr)
    {
        if (tr == null)
        {
            return null;
        }

        foreach (var enemyData in datas)
        {
            if (enemyData.ID == monsterID)
            {
                return Instantiate(enemyData.prefab, tr.position, tr.rotation);
            }
        }

        return null;
    }
}