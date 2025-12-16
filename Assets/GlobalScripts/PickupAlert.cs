using UnityEngine;
using TMPro;
using System.Collections;

public class PickupAlert : MonoBehaviour
{
    public TextMeshProUGUI alertText;
    public float moveSpeed = 20f;
    public float duration = 2f;
    public float fadeSpeed = 2f;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }


    public void Initialize(string message)
    {
        alertText.text = message;
        StartCoroutine(FadeAndMove());
    }

    private IEnumerator FadeAndMove()
    {
        float timer = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            // Move upward smoothly
            rectTransform.anchoredPosition = startPos + Vector2.up * moveSpeed * (timer / duration);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}
