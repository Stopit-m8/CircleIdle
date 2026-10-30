using UnityEngine;
using DG.Tweening;
using System;

public class Circle : MonoBehaviour
{
    private GameObject _centerCircle;
    private float _circleDuration = 2f;
    private int _circleValue = 1;
    private Vector3 _originalScale;

    public event Action OnComplete;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _centerCircle = GameObject.Find("CenterCircle");
    }

    private void OnEnable()
    {
        transform.DOKill();
        transform.localScale = _originalScale;
        transform.DOScale(_centerCircle.transform.localScale, _circleDuration).SetEase(Ease.Linear).OnComplete(CycleComplete);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void CycleComplete()
    {
        OnComplete?.Invoke();
        Debug.Log($"Cycle Complete, value = {_circleValue}, duration = {_circleDuration}");
        gameObject.SetActive(false);
    }

    public void Initialize(int circleValue, float circleDuration)
    {
        _circleValue = circleValue;
        _circleDuration = circleDuration;
    }
}
