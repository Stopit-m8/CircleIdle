using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Point On Hit")]
    [SerializeField] private int pointOnHit;
    [SerializeField] private int pointOnHitPrice = 10;
    [SerializeField] private float pointOnHitPriceMultiplier;
    [SerializeField] private int pointOnHitEffect = 2;

    [Header("Speed Circle")]
    [SerializeField] private float speedCircle;
    [SerializeField] private int speedCirclePrice = 50;
    [SerializeField] private float speedCirclePriceMultiplier;
    [SerializeField] private float speedCircleEffect = 0.05f;

    [Header("Golden Chance")]
    [SerializeField] private float goldenCircleChance;
    [SerializeField] private int goldenCircleChancePrice = 150;
    [SerializeField] private float goldenCircleChancePriceMultiplier;
    [SerializeField] private float goldenCircleChanceEffect = 0.015f;

    [Header("More Circle")]
    [SerializeField] private int moreCircle;
    [SerializeField] private int moreCirclePrice = 500;
    [SerializeField] private float moreCirclePriceMultiplier;
    [SerializeField] private int moreCircleEffect = 1;

    [Header("Global Multiplier")]
    [SerializeField] private float globalMultiplier;
    [SerializeField] private int globalMultiplierPrice = 1000;
    [SerializeField] private float globalMultiplierPriceMultiplier;
    [SerializeField] private float globalMultiplierEffect = 0.25f;

    [Header("Manager")]
    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private CurrencyManager currencyManager;

    private float _currentCurrency = 0f;
    private float _tempCurrency = 0f;


    public event Action<int> OnBuy;
    public event Action<int, float, float, int, float> OnUpgrade;
    public event Action<int, int, int, int, int> OnUpgradeBuy;

    private void Awake()
    {
        //load upgrades
    }

    private void OnEnable()
    {
        buttonManager.OnPointUpgraded += UpgradePointOnHit;
        buttonManager.OnSpeedCircleUpgraded += UpgradeSpeedCircle;
        buttonManager.OnGoldenChanceUpgraded += UpgradeGoldenCircleChance;
        buttonManager.OnMoreCircleUpgraded += UpgradeMoreCircle;
        buttonManager.OnGlobalMultiplierUpgraded += UpgradeGlobalMultiplier;
        currencyManager.OnUpdate += CurrencyUpdate;
    }

    private void OnDisable()
    {
        buttonManager.OnPointUpgraded -= UpgradePointOnHit;
        buttonManager.OnSpeedCircleUpgraded -= UpgradeSpeedCircle;
        buttonManager.OnGoldenChanceUpgraded -= UpgradeGoldenCircleChance;
        buttonManager.OnMoreCircleUpgraded -= UpgradeMoreCircle;
        buttonManager.OnGlobalMultiplierUpgraded -= UpgradeGlobalMultiplier;
        currencyManager.OnUpdate -= CurrencyUpdate;
    }

    private void CurrencyUpdate(int currentCurrency)
    {
        _currentCurrency = currentCurrency;
    }

    public void UpgradePointOnHit()
    {
        
        _tempCurrency = _currentCurrency;
        pointOnHitPrice = AdditionalPrice(pointOnHitPrice, pointOnHitPriceMultiplier);
        if (_tempCurrency != _currentCurrency)
        {
            pointOnHit += pointOnHitEffect;
            SendUpgrade();
        }
    }

    public void UpgradeSpeedCircle()
    {
        
        _tempCurrency = _currentCurrency;
        speedCirclePrice = AdditionalPrice(speedCirclePrice, speedCirclePriceMultiplier);
        if (_tempCurrency != _currentCurrency)
        {
            speedCircle *= speedCircleEffect;
            SendUpgrade();
        }
    }

    public void UpgradeGoldenCircleChance()
    {
        
        _tempCurrency = _currentCurrency;
        goldenCircleChancePrice = AdditionalPrice(goldenCircleChancePrice, goldenCircleChancePriceMultiplier);
        if (_tempCurrency != _currentCurrency)
        {
            goldenCircleChance *= goldenCircleChanceEffect;
            SendUpgrade();
        }
    }

    public void UpgradeMoreCircle()
    {
       
        _tempCurrency = _currentCurrency;
        moreCirclePrice = AdditionalPrice(moreCirclePrice, moreCirclePriceMultiplier);
        if (_tempCurrency != _currentCurrency)
        {
            moreCircle += moreCirclePrice;
            SendUpgrade();
        }
    }

    public void UpgradeGlobalMultiplier()
    {
        _tempCurrency = _currentCurrency;
        globalMultiplierPrice = AdditionalPrice(globalMultiplierPrice, globalMultiplierPriceMultiplier);
        if (_tempCurrency != _currentCurrency)
        {
            globalMultiplier *= globalMultiplierEffect;
            SendUpgrade();
        }
    }

    private int AdditionalPrice(int currentPrice, float priceMultiplier)
    {
        if (_currentCurrency >= currentPrice)
        {
            _currentCurrency -= currentPrice;
            OnBuy?.Invoke(-currentPrice);
            currentPrice *= Mathf.RoundToInt(priceMultiplier);
        }
        else
        {
            Debug.Log("Not Enough Moola");
        }

        return currentPrice;
    }

    private void SendUpgrade()
    {
        OnUpgrade?.Invoke(pointOnHit, speedCircle, goldenCircleChance, moreCircle, globalMultiplier);
        OnUpgradeBuy?.Invoke(pointOnHitPrice, speedCirclePrice, goldenCircleChancePrice, moreCirclePrice, globalMultiplierPrice);
    }
}
