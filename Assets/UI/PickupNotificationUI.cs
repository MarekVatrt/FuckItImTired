using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PickupNotificationUI : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI text;

    [Header("Animation")]
    public float duration = 1.2f;
    public float moveUpDistance = 40f;

    CanvasGroup canvasGroup;
    RectTransform rectTransform;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Setup(ItemData item, int amount)
    {
        icon.sprite = item.icon;
        text.text = $"+{amount} {item.itemName}";
        StartCoroutine(FadeAndMove());
    }

    IEnumerator FadeAndMove()
    {
        float elapsed = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = startPos + Vector2.up * moveUpDistance;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        Destroy(gameObject);
    }
}
