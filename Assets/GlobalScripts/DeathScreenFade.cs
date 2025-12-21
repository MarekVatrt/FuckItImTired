using UnityEngine;
using System.Collections;

public class DeathScreenFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Awake()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0f;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(start, end, time / fadeDuration);
            time += Time.unscaledDeltaTime; // works while paused
            yield return null;
        }

        canvasGroup.alpha = end;

        if (end == 0f)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
