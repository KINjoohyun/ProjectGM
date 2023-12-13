using UnityEngine;

[CreateAssetMenu(menuName = "IconSO")]
public class IconSO : ScriptableObject
{
    [Header("��� ID")]
    public int[] ID;

    [Header("�̹���")]
    public Sprite[] IMAGE;

    public Sprite GetSprite(int id)
    {
        for (int i = 0; i < ID.Length; i++)
        {
            if (ID[i] == id)
            {
                return IMAGE[i];
            }
        }
        return null;
    }
}
