using DG.Tweening;
using System;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private RectTransform upgradePanel;
    [SerializeField] private RectTransform upgradePanelEndRectTransform;
    private Vector2 originalAnchoredPosition;
    private bool _upgradeIsOpen = false;

    public event Action OnPointUpgraded;
    public event Action OnSpeedCircleUpgraded;
    public event Action OnGoldenChanceUpgraded;
    public event Action OnMoreCircleUpgraded;
    public event Action OnGlobalMultiplierUpgraded;

    private void Awake()
    {
        originalAnchoredPosition = upgradePanel.anchoredPosition;
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
            sequence.Append(upgradePanel.DOAnchorPos(originalAnchoredPosition, 1f).SetEase(Ease.OutBack));
            _upgradeIsOpen = false;
        }
        else
        {
            sequence.Append(upgradePanel.DOAnchorPos(position, 1f).SetEase(Ease.OutBack));
            _upgradeIsOpen = true;
        }
    }

}
