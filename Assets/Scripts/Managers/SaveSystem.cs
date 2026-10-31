using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    private string saveLocation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadData();
    }

    public void SaveData()
    {
        UpgradeManager upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        CurrencyManager currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
        UpgradeDatas upgradeDatas = new UpgradeDatas
        {
            currentPoint = currencyManager._currentCurency,
            pointOnHit = upgradeManager.pointOnHit,
            pointOnHitPrice = upgradeManager.pointOnHitPrice,
            circleSpeed = upgradeManager.speedCircle,
            circleSpeedPrice = upgradeManager.speedCirclePrice,
            goldenCircleChance = upgradeManager.goldenCircleChance,
            goldenCircleChancePrice = upgradeManager.goldenCircleChancePrice,
            moreCircle = upgradeManager.moreCircle,
            moreCirclePrice = upgradeManager.moreCirclePrice,
            globalMultiplier = upgradeManager.globalMultiplier,
            globalMultiplierPrice = upgradeManager.globalMultiplierPrice
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(upgradeDatas));
    }

    public void LoadData()
    {
        if (File.Exists(saveLocation))
        {
            Debug.Log("Load Data");
            UpgradeDatas upgradeDatas = JsonUtility.FromJson<UpgradeDatas>(File.ReadAllText(saveLocation));

            UpgradeManager upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
            CurrencyManager currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
            upgradeManager.LoadData(upgradeDatas.pointOnHit, upgradeDatas.pointOnHitPrice, upgradeDatas.circleSpeed, upgradeDatas.circleSpeedPrice, upgradeDatas.goldenCircleChance, upgradeDatas.goldenCircleChancePrice, upgradeDatas.moreCircle, upgradeDatas.moreCirclePrice, upgradeDatas.globalMultiplier, upgradeDatas.globalMultiplierPrice);
            currencyManager.LoadData(upgradeDatas.currentPoint);
        }
        else
        {
            SaveData();
        }
    }
}
