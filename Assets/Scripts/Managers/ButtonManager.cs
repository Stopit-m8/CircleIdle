using DG.Tweening;
using System;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public event Action OnPointUpgraded;
    public event Action OnSpeedCircleUpgraded;
    public event Action OnGoldenChanceUpgraded;
    public event Action OnMoreCircleUpgraded;
    public event Action OnGlobalMultiplierUpgraded;

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

    public void Fullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.fullScreen = true;
        Debug.Log("fullscreen");
    }

    public void BorderlessWindow()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.fullScreen = true;
        Debug.Log("borderless");
    }

    public void Windowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.fullScreen = false;
        Debug.Log("window");
    }
}
