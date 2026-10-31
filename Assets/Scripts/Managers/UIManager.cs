using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private UpgradeManager upgradeManager;

    [Header("Texts")]
    [SerializeField] private TMP_Text currencyText;
    //[SerializeField] private TMP_Text pointUpgradeText;
    //[SerializeField] private TMP_Text speedUpgradeText;
    //[SerializeField] private TMP_Text goldenUpgradeText;
    //[SerializeField] private TMP_Text moreCircleUpgradeText;
    //[SerializeField] private TMP_Text globalMultiplierUpgradeText;

    private void OnEnable()
    {
        currencyManager.OnUpdate += UpdateCurrencyUI;
        upgradeManager.OnUpgradeBuy += UpdateUpgradeUI;

    }

    private void UpdateCurrencyUI(int currentCurrency)
    {
        currencyText.text = currentCurrency.ToString();
    }

    private void UpdateUpgradeUI(int pointPrice, int speedPrice, int goldenPrice, int moreCirclePrice, int globalMultiplierPrice)
    {
        //pointUpgradeText.text = pointPrice.ToString();
        //speedUpgradeText.text = speedPrice.ToString();
        //goldenUpgradeText.text = goldenPrice.ToString();
        //moreCircleUpgradeText.text = moreCirclePrice.ToString();
        //globalMultiplierUpgradeText.text = globalMultiplierPrice.ToString();
    }
}
