using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PulseDestroyComponent : MonoBehaviour
{
    private RectTransform rectTransform;

    public float scaleAmount = 1.2f;  // Tamanho máximo da escala
    public float duration = 0.5f;     // Duração da animação

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void PulseAndDestroy()
    {
        // Escala inicial da UI
        Vector3 initialScale = rectTransform.localScale;

        // Cria uma sequência de animação para pulsar indefinidamente
        Sequence pulseSequence = DOTween.Sequence();
        pulseSequence.Append(rectTransform.DOScale(initialScale * scaleAmount, duration).SetEase(Ease.InOutSine))
                     .Append(rectTransform.DOScale(0, duration).SetEase(Ease.InOutSine)).OnComplete(() => Destroy(gameObject));
    }
}
