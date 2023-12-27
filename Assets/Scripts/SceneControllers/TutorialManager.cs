using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TutorialManager : MonoBehaviour
{
    private List<int> tutorialSteps = new List<int>();
    private int currentStepIndex = 0;


    // �����غ��� �ɰ� ���� ��� 3��° Ű�������� ��� ������ ����ɰŶ� �׵����� Ÿ�ӽ������� 1�� �Ǿ���Ѵ� �����
    // 5��° Ű�������� ����Ʈ�� ����Ǿ� ���� ������ �޶����ٰų� ���

    void Start()
    {
        PauseGame();
        InitializeTutorialSteps();
        ShowTutorialStep(tutorialSteps[currentStepIndex]);
    }

    public void NextStep()
    {
        currentStepIndex++; // ���� �ܰ� �ε����� �̵�
        if (currentStepIndex < tutorialSteps.Count)
        {
            ShowTutorialStep(tutorialSteps[currentStepIndex]);
        }
        else
        {
            Debug.Log("Ʃ�丮���� �������ϴ�.");
        }
    }

    private void ShowTutorialStep(int stepKey)
    {
        var table = CsvTableMgr.GetTable<DialogueTable>().dataTable;
        if (table.ContainsKey(stepKey))
        {
            var dialogueID = table[stepKey].dialogueID;
            var dialogueText = CsvTableMgr.GetTable<StringTable>().dataTable[dialogueID];

            Debug.Log(dialogueText);
        }
        else
        {
            Debug.Log("�ش� Ű ���� ���� �����Ͱ� �����ϴ�.");
        }
    }

    private void InitializeTutorialSteps()
    {
        var table = CsvTableMgr.GetTable<DialogueTable>().dataTable;

        tutorialSteps = table.Keys.ToList(); // ���̺��� ��� Ű�� ����Ʈ�� ��ȯ
        tutorialSteps.Sort();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
