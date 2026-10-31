using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private CircleManager circleManager;
    [SerializeField] private UpgradeManager upgradeManager;
    public long _currentCurency { get; private set; } = 0;

    public event Action<long> OnUpdate;

    private void Awake()
    {
        //load curency
        OnUpdate?.Invoke(_currentCurency);
    }

    public void LoadData(long currentCurency)
    {
        _currentCurency = currentCurency;
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

    private void UpdateCurrency(long newValue)
    {
        _currentCurency += newValue;
        OnUpdate?.Invoke(_currentCurency);
        SaveSystem.instance.SaveData();
    }
}
