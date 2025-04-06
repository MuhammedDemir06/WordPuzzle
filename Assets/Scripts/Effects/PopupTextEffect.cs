using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupTextEffect : MonoBehaviour
{
    public RectTransform textTransform;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.gameObject.SetActive(true);
        PlayPopupEffect("Welcome");
    }
    public void PlayPopupEffect(string newText)
    {
        text.text = newText;
        textTransform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(textTransform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack)); 
        seq.AppendInterval(0.3f); // Kısa süre kal
        seq.Append(textTransform.DOScale(0f, 0.4f).SetEase(Ease.InBack));

        seq.OnComplete(() => {
        });
    }
}
