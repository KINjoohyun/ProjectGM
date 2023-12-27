using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tutorialText;

    [SerializeField]
    private Image tutorialImage;

    private List<int> tutorialSteps = new List<int>();
    private int currentStepIndex = 0;

    

    // �����غ��� �ɰ� ���� ��� 3��° Ű�������� ��� ������ ����ɰŶ� �׵����� Ÿ�ӽ������� 1�� �Ǿ���Ѵ� �����
    // 5��° Ű�������� ����Ʈ�� ����Ǿ� ���� ������ �޶����ٰų� ���

    // ���� ��°Ŵ� Ÿ������ �Ҽ� ����
    // �׷��� ���� ��°Ŵ� �ڷ�ƾ���� �� �� �ۿ� ����

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

        Debug.Log(stepKey);

        if (stepKey == 1001007) // ����� �����ϴ°ǵ� �̷� ���� ���� �� ���� �ֳ�?
        {
            Debug.Log("123123123123123");

            StartCoroutine(MoveImage(tutorialImage.transform, 1.0f));
        }

        if (table.ContainsKey(stepKey))
        {
            var dialogueID = table[stepKey].dialogueID;
            var dialogueText = CsvTableMgr.GetTable<StringTable>().dataTable[dialogueID];

            Debug.Log(dialogueText);
            tutorialText.text = dialogueText;
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

    IEnumerator MoveImage(Transform imageTransform, float duration)
    {
        Vector3 startPosition = imageTransform.position; // ���� ��ġ
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 300, startPosition.z); // �� ��ġ (���� 100 ���� �̵�)

        float startTime = Time.realtimeSinceStartup;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.realtimeSinceStartup - startTime;
            imageTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            //..elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageTransform.position = endPosition; // ���� ��ġ�� ����
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
