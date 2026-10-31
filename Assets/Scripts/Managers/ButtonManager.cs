
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public event Action OnPointUpgraded;
    public event Action OnSpeedCircleUpgraded;
    public event Action OnGoldenChanceUpgraded;
    public event Action OnMoreCircleUpgraded;
    public event Action OnGlobalMultiplierUpgraded;

    [Header("Display Confirmation")]
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private TMP_Text countdownText;

    private const float confirmationDuration = 15f;
    private Coroutine _countdownCoroutine;

    private FullScreenMode _previousMode;
    private FullScreenMode _selectedMode;

    private int _previousWidth;
    private int _previousHeight;

    private void Start()
    {
        confirmationPanel.SetActive(false);

        if (PlayerPrefs.HasKey("ScreenMode"))
        {
            FullScreenMode savedMode =
                (FullScreenMode)PlayerPrefs.GetInt("ScreenMode");

            Screen.fullScreenMode = savedMode;
            Screen.fullScreen = savedMode != FullScreenMode.Windowed;
        }
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

    private void BeginScreenModeChange(FullScreenMode newMode)
    {
        if (_countdownCoroutine == null)
        {
            _previousMode = Screen.fullScreenMode;
            _previousWidth = Screen.width;
            _previousHeight = Screen.height;
        }

        _selectedMode = newMode;

        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
        }

        confirmationPanel.SetActive(true);
        _countdownCoroutine = StartCoroutine(ConfirmationCountdown());
    }

    public void Fullscreen()
    {
        BeginScreenModeChange(FullScreenMode.ExclusiveFullScreen);

        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.fullScreen = true;

        Debug.Log("fullscreen");
    }

    public void BorderlessWindow()
    {
        BeginScreenModeChange(FullScreenMode.FullScreenWindow);

        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.fullScreen = true;

        Debug.Log("borderless");
    }

    public void Windowed()
    {
        BeginScreenModeChange(FullScreenMode.Windowed);

        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.fullScreen = false;

        Debug.Log("window");
    }

    private IEnumerator ConfirmationCountdown()
    {
        float remainingTime = confirmationDuration;

        while (remainingTime > 0)
        {
            countdownText.text = $"Keep these display settings? Reverting in {Mathf.CeilToInt(remainingTime)} seconds.";

            yield return new WaitForSecondsRealtime(1f);

            remainingTime -= 1f;
        }

        Revert();
    }

    public void Confirm()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = null;
        }

        PlayerPrefs.SetInt("ScreenMode", (int)_selectedMode);
        PlayerPrefs.Save();

        confirmationPanel.SetActive(false);

        Debug.Log("Display settings confirmed!");
    }

    public void Revert()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = null;
        }

        Screen.SetResolution(_previousWidth, _previousHeight, _previousMode);

        confirmationPanel.SetActive(false);

        Debug.Log("Display settings reverted!");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
