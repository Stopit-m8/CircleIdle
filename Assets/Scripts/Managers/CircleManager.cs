using System;
using UnityEngine;
using DG.Tweening;

public class CircleManager : MonoBehaviour
{
    [SerializeField] private GameObject normalCirclePrefab;
    [SerializeField] private GameObject goldenCirclePrefab;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private GameObject circleCenter;
    private Vector3 _originalScale;
    private long _circleValue = 1;
    private float _circleSpeed = 2f;
    private float _goldenChance = 0.01f;
    private double _globalMultiplier = 0f;
    private float _circleSpawnRate = 2f;
    private float _circleSpawnTimer = 0f;
    private GameObject _circle;
    private long _finalValue;

    public event Action<long> OnCycleComplete;

    private void Start()
    {
        _circleSpawnTimer = _circleSpawnRate;
    }

    private void Awake()
    {
        //load circleStat
        _originalScale = circleCenter.transform.localScale;
    }

    private void OnEnable()
    {
        upgradeManager.OnUpgrade += UpdateStat;
    }

    private void OnDisable()
    {
        upgradeManager.OnUpgrade -= UpdateStat;
    }

    private void UpdateStat(long circleValue, float circleSpeed, float goldenChance, long circleCount, double globalmultiplier)
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
            long _goldenValue = (long)Mathf.Round(UnityEngine.Random.Range(5f, 10f) * _circleValue);
            _finalValue = _goldenValue + (long)Math.Round(_goldenValue * _globalMultiplier);
            _circle.GetComponent<Circle>().Initialize(_finalValue, _circleSpeed, true);
        }
        else
        {
            _circle = CirclePooling.Instance.SpawnFromPool("normal", this.transform.position);
            _finalValue = _circleValue + (long)Math.Round(_circleValue * _globalMultiplier);
            _circle.GetComponent<Circle>().Initialize(_finalValue, _circleSpeed, false);
        }
        
        _circle.SetActive(true);
        _circle.GetComponent<Circle>().OnComplete += CycleComplete;
        _circle.GetComponent<Circle>().OnComplete += PassOnPoints;
    }

    private void CycleComplete(Circle currCircle, long currCircleValue)
    {
        circleCenter.transform.DOKill();
        circleCenter.transform.localScale = _originalScale;
        circleCenter.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f);
        currCircle.OnComplete -= CycleComplete;
        currCircle.OnComplete -= PassOnPoints;
    }

    private void PassOnPoints(Circle currCircle, long currCircleValue)
    {
        OnCycleComplete?.Invoke(currCircleValue);
    }
}
