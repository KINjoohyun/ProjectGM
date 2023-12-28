using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneTranstrate : MonoBehaviour
{

    // public float waitTimeAfterFadeOut = 3.0f; 
    public float fadespeed = 1.0f;
    private bool isfading = false;
    public CanvasGroup fadeCanvasGroup;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isfading)
        {
            isfading = true;
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        while (fadeCanvasGroup.alpha < 1.0f)
        {
            fadeCanvasGroup.alpha += Time.deltaTime * fadespeed;
            yield return null;
        }

        // Optional: ������ �ð��� �߰��Ͽ� ���
       //  yield return new WaitForSeconds(waitTimeAfterFadeOut);

     
           SceneManager.LoadScene("Title"); // �� ��ȯ
        
        // Reset alpha after fade out
        // fadeCanvasGroup.alpha = 0;

       // isfading = false;
    }

}


