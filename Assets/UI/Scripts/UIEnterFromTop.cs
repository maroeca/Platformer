using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIEnterFromTop : MonoBehaviour
{
    public float duration = 1.5f;  // Duração da animação
    public float offset = 100f;  // Distância do objeto inicial fora da tela

    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        SetStartPosition();
    }

    public void ShowUIFromTop()
    {
        // Configura a posição inicial fora da tela (acima do Canvas)s
        SetStartPosition();

        // Move o objeto para o centro do Canvas
        rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(Ease.OutBounce);
    }

    public void HideUIFromBot()
    {
        // Configura a posição inicial fora da tela (acima do Canvas)s
        //SetStartPosition();
        startPosition.y -= offset;
        // Move o objeto para o centro do Canvas
        rectTransform.DOAnchorPos(startPosition, duration).SetEase(Ease.Linear);
    }

    private void SetStartPosition()
    {
        startPosition = rectTransform.anchoredPosition;
        startPosition.y += offset;
        rectTransform.anchoredPosition = startPosition;
    }
}
