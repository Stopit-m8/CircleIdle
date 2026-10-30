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

    public event Action<int> OnCycleComplete;

    private void Start()
    {
        _circleSpawnTimer = _circleSpawnRate;
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
        _circleSpawnRate = _circleSpeed / (float)circleCount;
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
            int _goldenValue = (int)UnityEngine.Random.Range(5f, 10f) * _circleValue;
            _circle.GetComponent<Circle>().Initialize(_goldenValue, _circleSpeed);
        }
        else
        {
            _circle = CirclePooling.Instance.SpawnFromPool("normal", this.transform.position);
            _circle.GetComponent<Circle>().Initialize(_circleValue, _circleSpeed);
        }
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
