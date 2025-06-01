using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSlideView : MonoBehaviour
{
    [Header("Scrolling")]
    public ScrollRect scrollRect;
    public RectTransform[] slides;
    public float slideSpeed = 5f;
    public float slideOffset = 100f;

    private Vector2 targetPosition;

    public void OnSlideClick(int slideIndex)
    {
        targetPosition = new Vector2(slides[slideIndex].anchoredPosition.x + slideOffset, 0);
        StopAllCoroutines();
        StartCoroutine(SmoothScroll());

        PositionArrows(slideIndex);
    }

    private void PositionArrows(int slideIndex)
    {
        if (slideIndex < 0 || slideIndex >= slides.Length) return;

        RectTransform slide = slides[slideIndex];
    }

    private IEnumerator SmoothScroll()
    {
        while (Vector2.Distance(scrollRect.content.anchoredPosition, -targetPosition) > 0.1f)
        {
            scrollRect.content.anchoredPosition =
                Vector2.Lerp(scrollRect.content.anchoredPosition,
                             -targetPosition,
                             Time.deltaTime * slideSpeed);
            yield return null;
        }
        scrollRect.content.anchoredPosition = -targetPosition;
    }
}

