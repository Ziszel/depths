using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackFadeTransition : MonoBehaviour
{
    private Image blackBackground;
    private float transitionDuration = 2.0f;

    private void Awake()
    {
        blackBackground = transform.Find("BlackBackground").GetComponent<Image>();
    }

    public void TriggerFadeToBlack()
    {
        StartCoroutine(FadeToBlack());
    }

    public void TriggerFadeFromBlack()
    {
        StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0;
        SetBackgroundTransparency(0);
        yield return new WaitForSeconds(1f);

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            SetBackgroundTransparency(Mathf.Clamp01(elapsedTime / transitionDuration));
            Debug.Log("FadeToBlack called. elapsed time: " + elapsedTime);
            yield return null;
        }

        GameManager.instance.DeactivateBlackFade();
    }

    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0;
        SetBackgroundTransparency(1);
        yield return new WaitForSecondsRealtime(0.5f);

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            SetBackgroundTransparency(Mathf.Clamp01(1 - (elapsedTime / transitionDuration)));
            Debug.Log("FadeFromBlack called. elapsed time: " + elapsedTime);
            yield return null;
        }

        GameManager.instance.DeactivateBlackFade();
    }

    private void SetBackgroundTransparency(float transparency)
    {
        Color newColour = blackBackground.color;
        newColour.a = transparency;
        blackBackground.color = newColour;
    }
}
