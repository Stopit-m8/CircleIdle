using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private CircleManager circleManager;
    [SerializeField] private UpgradeManager upgradeManager;
    private int _currentCurency = 0;

    public event Action<int> OnUpdate;

    private void Awake()
    {
        //load curency
        OnUpdate?.Invoke(_currentCurency);
    }

    private void OnEnable()
    {
        circleManager.OnCycleComplete += UpdateCurrency;
        upgradeManager.OnBuy += UpdateCurrency;
    }

    private void OnDisable()
    {
        circleManager.OnCycleComplete -= UpdateCurrency;
        upgradeManager.OnBuy -= UpdateCurrency;
    }

    private void UpdateCurrency(int newValue)
    {
        _currentCurency += newValue;
        OnUpdate?.Invoke(_currentCurency);
    }
}
