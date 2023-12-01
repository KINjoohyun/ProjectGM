#if UNITY_EDITOR
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

        newPatternName = EditorGUILayout.TextField("New Pattern Name", newPatternName);

        if (GUILayout.Button("Save Current Pattern"))
        {
            SaveAttackPattern(script, newPatternName, script.attackGrid);
        }

        if (script.savedPatterns.Count > 0)
        {
            EditorGUILayout.LabelField("Saved Patterns: ");
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
            Debug.LogError("���� �̸��� Empty");
            return;
        }

        // ������ �̸��� ���� ã��
        var existingPattern = script.savedPatterns.Find(p => p.patternName == patternName);

        if (existingPattern != null)
        {
            existingPattern.pattern = patternData; // �̹� �����ϴ� ������ ���� �����
        }
        else
        {
            script.savedPatterns.Add(new AttackPattern(patternName, patternData));
        }

        EditorUtility.SetDirty(script);
    }
}
#endif
