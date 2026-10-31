using System;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    [SerializeField] private GameObject normalCirclePrefab;
    [SerializeField] private GameObject goldenCirclePrefab;
    [SerializeField] private UpgradeManager upgradeManager;
    private int _circleValue = 1;
    private float _circleSpeed = 2f;
    private float _goldenChance = 0.01f;
    private float _globalMultiplier = 0f;
    private float _circleSpawnRate = 2f;
    private float _circleSpawnTimer = 0f;
    private GameObject _circle;
    private int _finalValue;

    public event Action<int> OnCycleComplete;

    private void Start()
    {
        _circleSpawnTimer = _circleSpawnRate;
    }

    private void Awake()
    {
        //load circleStat
    }

    private void OnEnable()
    {
        upgradeManager.OnUpgrade += UpdateStat;
    }

    private void OnDisable()
    {
        upgradeManager.OnUpgrade -= UpdateStat;
    }

    private void UpdateStat(int circleValue, float circleSpeed, float goldenChance, int circleCount, float globalmultiplier)
    {
        _circleValue = circleValue;
        _circleSpeed = circleSpeed;
        _goldenChance = goldenChance;
        _circleSpawnRate = _circleSpeed / circleCount;
        Debug.Log($"circle Spawn rate = {_circleSpawnRate}, Circle Speed = {_circleSpeed}, Circle Count = {circleCount}");
        _globalMultiplier = globalmultiplier;
    }

    private void Update()
    {
        _circleSpawnTimer += Time.deltaTime;
        if (_circleSpawnTimer >= _circleSpawnRate)
        {
            SpawnCircle();
            _circleSpawnTimer = 0f;
        }
    }

    private void SpawnCircle()
    {
        if (UnityEngine.Random.value <= _goldenChance)
        {
            _circle = CirclePooling.Instance.SpawnFromPool("golden", this.transform.position);
            int _goldenValue = Mathf.RoundToInt(UnityEngine.Random.Range(5f, 10f) * _circleValue);
            _finalValue = _goldenValue + (Mathf.RoundToInt(_goldenValue * _globalMultiplier));
        }
        else
        {
            _circle = CirclePooling.Instance.SpawnFromPool("normal", this.transform.position);
            _finalValue = _circleValue + (Mathf.RoundToInt(_circleValue * _globalMultiplier));
        }
        _circle.GetComponent<Circle>().Initialize(_finalValue, _circleSpeed);
        _circle.SetActive(true);
        _circle.GetComponent<Circle>().OnComplete += CycleComplete;
        _circle.GetComponent<Circle>().OnComplete += PassOnPoints;
    }

    private void CycleComplete(Circle currCircle, int currCircleValue)
    {
        currCircle.OnComplete -= CycleComplete;
        currCircle.OnComplete -= PassOnPoints;
    }

    private void PassOnPoints(Circle currCircle, int currCircleValue)
    {
        OnCycleComplete?.Invoke(currCircleValue);
    }
}
