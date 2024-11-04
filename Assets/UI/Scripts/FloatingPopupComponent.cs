using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingPopupComponent : MonoBehaviour
{
    public float floatDistance = 50f;  // Dist�ncia que o objeto vai flutuar para cima
    public float duration = 1f;        // Dura��o da anima��o
    public float startOpacity = 1f;    // Opacidade inicial
    public float endOpacity = 0f;      // Opacidade final

    private CanvasGroup canvasGroup;

    void Start()
    {
        // Adiciona ou obt�m o CanvasGroup para controlar a opacidade
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = startOpacity;

        // Cria a anima��o para flutuar e desaparecer
        Sequence floatAndFadeSequence = DOTween.Sequence();

        // Anima a posi��o para subir com uma curva mais suave
        floatAndFadeSequence.Append(transform.DOMoveY(transform.position.y + floatDistance, duration).SetEase(Ease.OutQuad));

        // Anima a opacidade para desaparecer ao longo da mesma dura��o
        floatAndFadeSequence.Join(canvasGroup.DOFade(endOpacity, duration));

        // Destroi o objeto ao final da anima��o
        floatAndFadeSequence.OnComplete(() => Destroy(gameObject));
    }
}
