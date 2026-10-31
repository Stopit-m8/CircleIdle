using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BigButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private RectTransform panelEndRectTransformHover;
    [SerializeField] private RectTransform panelEndRectTransform;
    private Vector2 _originalAnchoredPosition;
    private Vector2 _position;
    private Vector2 _positionEnd;
    private bool _isOpen = false;
    private bool _isClicked = false;

    private void Awake()
    {
        _originalAnchoredPosition = panel.anchoredPosition;
        _position = panelEndRectTransformHover.anchoredPosition;
        _positionEnd = panelEndRectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isClicked)
        {
            return;
        }
        panel.DOKill();
        panel.DOAnchorPos(panelEndRectTransformHover.anchoredPosition, 1f).SetEase(Ease.OutBack, 0.7f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isClicked)
        {
            return;
        }
        panel.DOKill();
        panel.DOAnchorPos(_originalAnchoredPosition, 1f).SetEase(Ease.OutBack, 0.7f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isOpen)
        {
            panel.DOKill();
            panel.DOAnchorPos(_originalAnchoredPosition, 1f).SetEase(Ease.OutBack, 0.7f);
            _isOpen = false;
            _isClicked = false;
        }
        else
        {
            panel.DOKill();
            panel.DOAnchorPos(_positionEnd, 1f).SetEase(Ease.OutBack);
            _isOpen = true;
            _isClicked = true;
        }
    }
}
