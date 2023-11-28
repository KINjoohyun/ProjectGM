using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyAI))]
public class EnemyAIEditor : Editor
{
    private string newPatternName = "";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemyAI script = (EnemyAI)target;

        // �� ���� �̸� �Է� �ʵ�
        newPatternName = EditorGUILayout.TextField("New Pattern Name", newPatternName);

        // ���� ��ư
        if (GUILayout.Button("Save Current Pattern"))
        {
            SaveAttackPattern(script, newPatternName, script.attackGrid);
        }

        // ����� ���� ��� ǥ�� (�ɼ�)
        if (script.savedPatterns.Count > 0)
        {
            EditorGUILayout.LabelField("Saved Patterns:");
            foreach (var pattern in script.savedPatterns)
            {
                EditorGUILayout.LabelField(pattern.patternName);
            }
        }
    }

    private void SaveAttackPattern(EnemyAI script, string patternName, bool[] patternData)
    {
        if (string.IsNullOrWhiteSpace(patternName))
        {
            Debug.LogError("Pattern name is empty");
            return;
        }

        // ������ �̸��� ���� ã��
        var existingPattern = script.savedPatterns.Find(p => p.patternName == patternName);

        if (existingPattern != null)
        {
            // �̹� �����ϴ� �����̸� ������ ������Ʈ
            existingPattern.pattern = patternData;
        }
        else
        {
            // ���ο� �����̸� ����Ʈ�� �߰�
            script.savedPatterns.Add(new AttackPattern(patternName, patternData));
        }

        // ���� ���� ����
        EditorUtility.SetDirty(script);
    }
}
