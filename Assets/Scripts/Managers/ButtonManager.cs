using DG.Tweening;
using System;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("Upgrade")]
    [SerializeField] private RectTransform upgradePanel;
    [SerializeField] private RectTransform upgradePanelEndRectTransform;

    [Header("Setting")]
    [SerializeField] private RectTransform settingPanel;
    [SerializeField] private RectTransform settingPanelEndRectTransform;
    private Vector2 originalUpgradeAnchoredPosition;
    private Vector2 originalSettingAnchoredPosition;
    private bool _upgradeIsOpen = false;
    private bool _settingIsOpen = false;

    public event Action OnPointUpgraded;
    public event Action OnSpeedCircleUpgraded;
    public event Action OnGoldenChanceUpgraded;
    public event Action OnMoreCircleUpgraded;
    public event Action OnGlobalMultiplierUpgraded;

    private void Awake()
    {
        originalUpgradeAnchoredPosition = upgradePanel.anchoredPosition;
        originalSettingAnchoredPosition = settingPanel.anchoredPosition;
    }

    public void PointUpgrade()
    {
        OnPointUpgraded?.Invoke();
    }

    public void SpeedCircleUpgrade()
    {
        OnSpeedCircleUpgraded?.Invoke();
    }

    public void GoldenChanceUpgrade()
    {
        OnGoldenChanceUpgraded?.Invoke();
    }

    public void MoreCircleUpgrade()
    {
        OnMoreCircleUpgraded?.Invoke();
    }

    public void GlobalMultiplierUpgrade()
    {
        OnGlobalMultiplierUpgraded?.Invoke();
    }

    public void OpenCloseUpgrade()
    {
        Vector2 position = upgradePanelEndRectTransform.anchoredPosition;
        Sequence sequence = DOTween.Sequence();
        if (_upgradeIsOpen)
        {
            sequence.Append(upgradePanel.DOAnchorPos(originalUpgradeAnchoredPosition, 1f).SetEase(Ease.OutBack, 0.7f));
            _upgradeIsOpen = false;
        }
        else
        {
            sequence.Append(upgradePanel.DOAnchorPos(position, 1f).SetEase(Ease.OutBack));
            _upgradeIsOpen = true;
        }
    }

    public void OpenCloseSetting()
    {
        Vector2 position = settingPanelEndRectTransform.anchoredPosition;
        Sequence sequence = DOTween.Sequence();
        if (_settingIsOpen)
        {
            sequence.Append(settingPanel.DOAnchorPos(originalSettingAnchoredPosition, 1f).SetEase(Ease.OutBack, 0.7f));
            _settingIsOpen = false;
        }
        else
        {
            sequence.Append(settingPanel.DOAnchorPos(position, 1f).SetEase(Ease.OutBack));
            _settingIsOpen = true;
        }
    }

}
