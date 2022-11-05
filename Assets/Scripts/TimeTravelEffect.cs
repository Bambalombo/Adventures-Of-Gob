using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeTravelEffect : MonoBehaviour
{
    public RectTransform timeTextRT;
    private TextMeshProUGUI textMeshProUGUI;

    private float desiredY = 55;

    public float transitionSpeed = 5000f;

    private void Start()
    {
        timeTextRT = GameObject.Find("TimeText").GetComponent<RectTransform>();
        textMeshProUGUI = timeTextRT.gameObject.GetComponent<TextMeshProUGUI>();

        Invoke(nameof(InvokedFind), 0.2f);
    }

    private void InvokedFind()
    {
        timeTextRT = GameObject.Find("TimeText").GetComponent<RectTransform>();
        textMeshProUGUI = timeTextRT.gameObject.GetComponent<TextMeshProUGUI>();

        if (SceneManager.GetActiveScene().buildIndex == 3 || (SceneManager.GetActiveScene().buildIndex == 4)) // ny linus
        {
            GoTo2021();
        }
        else
        {
            GoTo2050();
        }
    }
    
    public void GoTo2050()
    {
        StartCoroutine(LerpTo2050());
    }

    public void GoTo2021()
    {
        StartCoroutine(LerpTo2021());
    }

    public IEnumerator LerpTo2050()
    {
        float curY = 0;
        float softValue = 0;

        while (curY < desiredY)
        {
            curY += 6;
            softValue = 100 - curY;
            softValue = softValue < 0 ? softValue = 0 : softValue = softValue;
            softValue /= 100f;

            textMeshProUGUI.fontSharedMaterial.SetFloat("_OutlineSoftness", softValue * 2);
            textMeshProUGUI.fontSharedMaterial.SetFloat("_OutlineSoftness", softValue * 2);
 
            timeTextRT.offsetMin = new Vector2(0, -curY);
            timeTextRT.offsetMax = new Vector2(0, curY);

            yield return new WaitForSeconds(0.0001f / transitionSpeed);
        }
    }

    public IEnumerator LerpTo2021()
    {
        float curY = desiredY;
        float softValue = 100;

        while (curY > 0)
        {
            curY -= 6;
            softValue = curY;
            softValue = softValue < 0 ? softValue = 0 : softValue = softValue;
            softValue /= 100f;

            if (textMeshProUGUI != null)
                textMeshProUGUI.fontSharedMaterial.SetFloat("_OutlineSoftness", softValue * 2);

            timeTextRT = GameObject.Find("TimeText").GetComponent<RectTransform>();

            timeTextRT.offsetMin = new Vector2(0, -curY);
            timeTextRT.offsetMax = new Vector2(0, curY);

            yield return new WaitForSeconds(0.0001f / transitionSpeed);
        }
    }
}