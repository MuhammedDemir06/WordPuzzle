using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlideTextEffect : MonoBehaviour
{
    public RectTransform textTransform;
    public TextMeshProUGUI uiText;
    private void Start()
    {
        uiText.gameObject.SetActive(true);

        Color c = uiText.color;
        c.a = 0;
        uiText.color = c;
    }
    public void PlaySlideEffect(string newText)
    {
        uiText.text = newText;

        Sequence seq = DOTween.Sequence();

        Vector2 startPos = new Vector2(-Screen.width, textTransform.anchoredPosition.y);
        Vector2 midPos = new Vector2(0, textTransform.anchoredPosition.y);
        Vector2 endPos = startPos;

        textTransform.anchoredPosition = startPos;

        Color originalColor = uiText.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        Color visibleColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        uiText.color = transparentColor;

        seq.Append(textTransform.DOAnchorPos(midPos, 1f).SetEase(Ease.OutCubic));
        seq.Join(DOTween.To(() => uiText.color, x => uiText.color = x, visibleColor, 1f));

        seq.AppendInterval(0.5f);

        seq.Append(textTransform.DOAnchorPos(endPos, 1f).SetEase(Ease.InCubic));
        seq.Join(DOTween.To(() => uiText.color, x => uiText.color = x, transparentColor, 1f));

        seq.OnComplete(() =>
        {
            textTransform.anchoredPosition = startPos;
            uiText.color = transparentColor;
        });
    }
}
