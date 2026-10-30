using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private int _pointOnHit;
    private float _speedCircle;
    private float _goldenCircleChance;
    private int _moreCircle;
    private float _globalMultiplier;


    public event Action<int> OnBuy;
    public event Action<int, float, float, int, float> OnUpgrade;

    private void Awake()
    {
        //load upgrades
    }

    public void UpgradePointOnHit()
    {
        SendUpgrade();
    }

    public void UpgradeSpeedCircle()
    {
        SendUpgrade();
    }

    public void UpgradeGoldenCircleChance()
    {
        SendUpgrade();
    }

    public void UpgradeMoreCircle()
    {
        SendUpgrade();
    }

    public void UpgradeGlobalMultiplier()
    {
        SendUpgrade();
    }

    private void SendUpgrade()
    {
        OnUpgrade?.Invoke(_pointOnHit, _speedCircle, _goldenCircleChance, _moreCircle, _globalMultiplier);
    }
}
