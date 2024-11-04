using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PulseDestroyComponent : MonoBehaviour
{
    private RectTransform rectTransform;

    public float scaleAmount = 1.2f;  // Tamanho m�ximo da escala
    public float duration = 0.5f;     // Dura��o da anima��o

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void PulseAndDestroy()
    {
        // Escala inicial da UI
        Vector3 initialScale = rectTransform.localScale;

        // Cria uma sequ�ncia de anima��o para pulsar indefinidamente
        Sequence pulseSequence = DOTween.Sequence();
        pulseSequence.Append(rectTransform.DOScale(initialScale * scaleAmount, duration).SetEase(Ease.InOutSine))
                     .Append(rectTransform.DOScale(0, duration).SetEase(Ease.InOutSine)).OnComplete(() => Destroy(gameObject));
    }
}
