using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalePopupComponent : MonoBehaviour
{
    public float duration = 1f;  // Duração da animação
    public Ease easeType = Ease.OutBack;  // Tipo de easing para um efeito mais suave
    public float delay = 0;

    void Start()
    {
        // Define a escala inicial como zero
        transform.localScale = Vector3.zero;

        // Anima a escala de 0 até 1
        transform.DOScale(Vector3.one, duration).SetEase(easeType).SetDelay(delay);
    }
}
