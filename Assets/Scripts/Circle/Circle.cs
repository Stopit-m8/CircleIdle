using UnityEngine;
using DG.Tweening;
using System;
using Unity.VisualScripting;

public class Circle : MonoBehaviour
{
    private GameObject _centerCircle;
    private float _circleDuration = 2f;
    private long _circleValue = 1;
    private Vector3 _originalScale;
    private bool _isGolden = false;

    public event Action<Circle, long> OnComplete;

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
        if (_isGolden)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.GoldCirclePop);
        }
        else
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.CirclePop);
        }
            
        OnComplete?.Invoke(this, _circleValue);
        Debug.Log($"Cycle Complete, value = {_circleValue}, duration = {_circleDuration}");
        gameObject.SetActive(false);
    }

    public void Initialize(long circleValue, float circleDuration, bool isGolden)
    {
        _circleValue = circleValue;
        _circleDuration = circleDuration;
        _isGolden = isGolden;
    }
}
