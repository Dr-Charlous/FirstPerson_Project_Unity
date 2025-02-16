using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public PlacementInteraction PlacementInt = new PlacementInteraction();
    public ObjectInfos.ObjectSubType SubType; 
    public bool IsReplace = true;
    public bool IsBreak = false;

    [SerializeField] float _breakTimer = 60f;
    [SerializeField] [Tooltip("Min/Max range random")] Vector2 _breakTimerRandomRange = new Vector2(-10, 10);
    [SerializeField] GameObject _objectMeshReference;
    [SerializeField] GameObject _particuleEffect;

    float _timer = 0;
    float _timerTarget = 0;

    private void Start()
    {
        if (SubType == ObjectInfos.ObjectSubType.Fuse || SubType == ObjectInfos.ObjectSubType.Pipe)
            BreakTypeStart();

        if (SubType == ObjectInfos.ObjectSubType.Fuel)
            FuelTypeStart();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePause)
        {
            if (SubType == ObjectInfos.ObjectSubType.Fuse || SubType == ObjectInfos.ObjectSubType.Pipe)
                BreakTypeUpdate();

            if (SubType == ObjectInfos.ObjectSubType.Fuel)
                FuelTypeUpdate();
        }
    }

    public void Repair()
    {
        if (SubType == ObjectInfos.ObjectSubType.Fuse || SubType == ObjectInfos.ObjectSubType.Pipe)
            BreakRepair();

        if (SubType == ObjectInfos.ObjectSubType.Fuel)
            FuelRepair();
    }

    #region BreakType
    void BreakTypeStart()
    {
        _objectMeshReference.SetActive(true);
        _particuleEffect.SetActive(false);
        _timerTarget = _breakTimer + Random.Range(_breakTimerRandomRange.x, _breakTimerRandomRange.y);
    }

    void BreakTypeUpdate()
    {
        if (_timer >= _timerTarget)
        {
            IsBreak = true;
            IsReplace = false;
            _objectMeshReference.SetActive(false);
            _particuleEffect.SetActive(true);
        }
        else
            _timer += Time.deltaTime;
    }

    void BreakRepair()
    {
        IsBreak = false;
        IsReplace = true;
        _timer = 0;
        _timerTarget = _breakTimer + Random.Range(_breakTimerRandomRange.x, _breakTimerRandomRange.y);
        _objectMeshReference.SetActive(true);
        _particuleEffect.SetActive(false);
    }
    #endregion

    #region FuelType
    void FuelTypeStart()
    {
        IsBreak = false;
        IsReplace = false;
        _objectMeshReference.SetActive(true);
        _particuleEffect.SetActive(true);
        _timerTarget = _breakTimer + _breakTimerRandomRange.y;
    }

    void FuelTypeUpdate()
    {
        if (_timer >= _timerTarget)
        {
            IsBreak = true;
            _particuleEffect.SetActive(false);
        }
        else
            _timer += Time.deltaTime;
    }

    void FuelRepair()
    {
        if (IsBreak)
        {
            _timer = 0;
            _timerTarget = _breakTimer + Random.Range(_breakTimerRandomRange.x, _breakTimerRandomRange.y);
            _particuleEffect.SetActive(true);
        }
        else
        {
            _timerTarget -= _timer;
            _timerTarget += _breakTimer + Random.Range(_breakTimerRandomRange.x, _breakTimerRandomRange.y);
            _timer = 0;
        }

        IsBreak = false;
        IsReplace = false;
    }
    #endregion
}