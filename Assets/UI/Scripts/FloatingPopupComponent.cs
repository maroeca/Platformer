using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingPopupComponent : MonoBehaviour
{
    public float floatDistance = 50f;  // Distância que o objeto vai flutuar para cima
    public float duration = 1f;        // Duração da animação
    public float startOpacity = 1f;    // Opacidade inicial
    public float endOpacity = 0f;      // Opacidade final

    private CanvasGroup canvasGroup;

    void Start()
    {
        // Adiciona ou obtém o CanvasGroup para controlar a opacidade
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = startOpacity;

        // Cria a animação para flutuar e desaparecer
        Sequence floatAndFadeSequence = DOTween.Sequence();

        // Anima a posição para subir com uma curva mais suave
        floatAndFadeSequence.Append(transform.DOMoveY(transform.position.y + floatDistance, duration).SetEase(Ease.OutQuad));

        // Anima a opacidade para desaparecer ao longo da mesma duração
        floatAndFadeSequence.Join(canvasGroup.DOFade(endOpacity, duration));

        // Destroi o objeto ao final da animação
        floatAndFadeSequence.OnComplete(() => Destroy(gameObject));
    }
}
