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

    [SerializeField]
    private Button touchArea;

    private List<int> tutorialSteps = new List<int>();
    private int currentStepIndex = 0;

    // �����غ��� �ɰ� ���� ��� 3��° Ű�������� ��� ������ ����ɰŶ� �׵����� Ÿ�ӽ������� 1�� �Ǿ���Ѵ� �����
    // 5��° Ű�������� ����Ʈ�� ����Ǿ� ���� ������ �޶����ٰų� ���

    // ���� ��°Ŵ� Ÿ������ �Ҽ� ����
    // �׷��� ���� ��°Ŵ� �ڷ�ƾ���� �� �� �ۿ� ����

    void Start()
    {
        touchArea.interactable = true;

        PauseGame();
        InitializeTutorial();
        ShowTutorialStep(tutorialSteps[currentStepIndex]);
    }

    public void NextStep()
    {
        currentStepIndex++;
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

        //Debug.Log(stepKey);

        if (stepKey == 1001007)
        {

            StartCoroutine(MoveImage(tutorialImage.transform, 1.0f));
        }

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

            if(table[stepKey].dialType == 2)
            {
                Debug.Log("���̾�Ÿ�� 2 ����");

                StartCoroutine(DisableInputForSeconds(3.0f));
            }

        }
        else
        {
            Debug.Log("�ش� Ű ���� ���� �����Ͱ� �����ϴ�.");
        }
    }

    private void InitializeTutorial()
    {
        var table = CsvTableMgr.GetTable<DialogueTable>().dataTable;

        tutorialSteps.Clear();


        foreach (var pair in table)
        {
            if (pair.Value.dialType == 1)
            {
                tutorialSteps.Add(pair.Key);
            }
        }

        //tutorialSteps = table.Keys.ToList();
        tutorialSteps.Sort();
    }

    IEnumerator MoveImage(Transform imageTransform, float duration)
    {
        Vector3 startPosition = imageTransform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 300, startPosition.z);

        float startTime = Time.realtimeSinceStartup;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.realtimeSinceStartup - startTime;
            imageTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            yield return null;
        }

        imageTransform.position = endPosition;
    }

    IEnumerator DisableInputForSeconds(float seconds)
    {
        touchArea.interactable = false;

        yield return new WaitForSecondsRealtime(seconds);

        touchArea.interactable = true;
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
