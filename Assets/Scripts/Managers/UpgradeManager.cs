using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Point On Hit")]
    [SerializeField] private long pointOnHit = 1;
    [SerializeField] private long pointOnHitPrice = 10;
    [SerializeField] private float pointOnHitPriceMultiplier;
    [SerializeField] private int pointOnHitEffect = 2;

    [Header("Speed Circle")]
    [SerializeField] private float speedCircle = 2f;
    [SerializeField] private long speedCirclePrice = 50;
    [SerializeField] private float speedCirclePriceMultiplier;
    [SerializeField] private float speedCircleEffect = 0.05f;
    [SerializeField] private float speedCap = 0.1f;
    private bool _speedIsCap = false;

    [Header("Golden Chance")]
    [SerializeField] private float goldenCircleChance = 0.01f;
    [SerializeField] private long goldenCircleChancePrice = 150;
    [SerializeField] private float goldenCircleChancePriceMultiplier;
    [SerializeField] private float goldenCircleChanceEffect = 0.015f;
    [SerializeField] private float goldenCap = 0.35f;
    private bool _goldIsCap = false;

    [Header("More Circle")]
    [SerializeField] private long moreCircle = 1;
    [SerializeField] private long moreCirclePrice = 500;
    [SerializeField] private float moreCirclePriceMultiplier;
    [SerializeField] private int moreCircleEffect = 1;

    [Header("Global Multiplier")]
    [SerializeField] private double globalMultiplier = 0f;
    [SerializeField] private long globalMultiplierPrice = 1000;
    [SerializeField] private float globalMultiplierPriceMultiplier;
    [SerializeField] private float globalMultiplierEffect = 0.25f;

    [Header("Manager")]
    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private CurrencyManager currencyManager;

    private float _currentCurrency = 0f;
    private float _tempCurrency = 0f;


    public event Action<long> OnBuy;
    public event Action<long, float, float, long, double> OnUpgrade;
    public event Action<long, long, long, long, long> OnUpgradeBuy;

    private void Awake()
    {
        //load upgrades
        SendUpgrade();
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

    private void CurrencyUpdate(long currentCurrency)
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
        if (_tempCurrency != _currentCurrency && speedCircle != speedCap)
        {
            speedCircle -= speedCircleEffect;
            if (speedCircle <= speedCap)
            {
                speedCircle = speedCap;
                _speedIsCap = true;
                speedCirclePrice = 0;
            }
            SendUpgrade();
        }
    }

    public void UpgradeGoldenCircleChance()
    {
        
        _tempCurrency = _currentCurrency;
        goldenCircleChancePrice = AdditionalPrice(goldenCircleChancePrice, goldenCircleChancePriceMultiplier);
        if (_tempCurrency != _currentCurrency && goldenCircleChance != goldenCap)
        {
            goldenCircleChance += goldenCircleChanceEffect;
            if (goldenCircleChance >= goldenCap)
            {
                goldenCircleChance = goldenCap;
                _goldIsCap = true;
                goldenCircleChancePrice = 0;
            }
            SendUpgrade();
        }
    }

    public void UpgradeMoreCircle()
    {
       
        _tempCurrency = _currentCurrency;
        moreCirclePrice = AdditionalPrice(moreCirclePrice, moreCirclePriceMultiplier);
        if (_tempCurrency != _currentCurrency)
        {
            moreCircle += moreCircleEffect;
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

    private long AdditionalPrice(long currentPrice, float priceMultiplier)
    {
        if (_currentCurrency >= currentPrice)
        {
            _currentCurrency -= currentPrice;
            OnBuy?.Invoke(-currentPrice);
            long newPrice = Mathf.RoundToInt(currentPrice * priceMultiplier);
            return newPrice;
        }
        else
        {
            long newPrice = currentPrice;
            Debug.Log("Not Enough Moola");
            return newPrice;
        }

        
    }

    private void SendUpgrade()
    {
        OnUpgrade?.Invoke(pointOnHit, speedCircle, goldenCircleChance, moreCircle, globalMultiplier);
        OnUpgradeBuy?.Invoke(pointOnHitPrice, speedCirclePrice, goldenCircleChancePrice, moreCirclePrice, globalMultiplierPrice);
    }
}
