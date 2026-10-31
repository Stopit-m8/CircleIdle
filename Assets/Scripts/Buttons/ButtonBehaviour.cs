using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image _image;
    private Vector3 _scale;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _scale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(_scale.x + 0.2f, _scale.y + 0.2f, _scale.z + 0.2f), 0.5f).SetUpdate(true);
        Debug.Log("bigger");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();

        transform.DOScale(_scale, 0.5f).SetUpdate(true);
        Debug.Log("smaller");


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOKill();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(new Vector3(_scale.x + 0.3f, _scale.y + 0.3f, _scale.z + 0.3f), 0.3f).SetEase(Ease.OutBack).SetUpdate(true))
            .Append(transform.DOScale(_scale, 0.3f).SetUpdate(true));
    }
}
