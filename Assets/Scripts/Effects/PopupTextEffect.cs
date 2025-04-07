using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupTextEffect : MonoBehaviour
{
    public RectTransform effectTransform;
    [Header("For Text")]
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.gameObject.SetActive(true);
        PlayPopupEffectForText("Welcome");
    }
    //For Text
    public void PlayPopupEffectForText(string newText)
    {
        text.text = newText;
        PlayPopupEffect();
    }
    public void PlayPopupEffect()
    {
        effectTransform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(effectTransform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack));
        seq.AppendInterval(0.3f); // Kısa süre kal
        seq.Append(effectTransform.DOScale(0f, 0.4f).SetEase(Ease.InBack));

        seq.OnComplete(() => {
        });
    }
}
