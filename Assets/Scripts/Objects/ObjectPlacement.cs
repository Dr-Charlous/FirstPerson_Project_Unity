using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public PlacementInteraction PlacementInt = new PlacementInteraction();
    public ObjectInfos.ObjectSubType SubType;
    public bool IsReplace = true;
    public bool IsBreak = false;
    public bool IsMeshDisplayOnRepair = true;

    [SerializeField] float _breakTimer = 60f;
    [SerializeField][Tooltip("Min/Max range random")] Vector2 _breakTimerRandomRange = new Vector2(-10, 10);
    [SerializeField] GameObject _objectMeshReference;
    [SerializeField] GameObject _particuleEffect;

    float _timer = 0;
    float _timerTarget = 0;

    private void Start()
    {
        if (SubType == ObjectInfos.ObjectSubType.Weapon || SubType == ObjectInfos.ObjectSubType.Other)
            BreakTypeStart();

        if (SubType == ObjectInfos.ObjectSubType.Fuel)
            FuelTypeStart();
    }

    private void Update()
    {
        if (!PlayerManager.Instance.UiManager.IsGamePause)
        {
            if (SubType == ObjectInfos.ObjectSubType.Weapon || SubType == ObjectInfos.ObjectSubType.Other)
                BreakTypeUpdate();

            if (SubType == ObjectInfos.ObjectSubType.Fuel)
                FuelTypeUpdate();
        }
    }

    public void Repair()
    {
        if (SubType == ObjectInfos.ObjectSubType.Weapon || SubType == ObjectInfos.ObjectSubType.Other)
            BreakRepair();

        if (SubType == ObjectInfos.ObjectSubType.Fuel)
            FuelRepair();
    }

    #region BreakType
    void BreakTypeStart()
    {
        DisplayRepair();

        _timerTarget = _breakTimer + Random.Range(_breakTimerRandomRange.x, _breakTimerRandomRange.y);
    }

    void BreakTypeUpdate()
    {
        if (_timer >= _timerTarget)
        {
            IsBreak = true;
            IsReplace = false;

            DisplayRepair();
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

        if (IsMeshDisplayOnRepair)
        {
            if (_objectMeshReference != null)
                _objectMeshReference.SetActive(true);
            if (_particuleEffect != null)
                _particuleEffect.SetActive(false);
        }
        else
        {
            if (_objectMeshReference != null)
                _objectMeshReference.SetActive(false);
            if (_particuleEffect != null)
                _particuleEffect.SetActive(true);
        }
    }

    void DisplayRepair()
    {
        if (IsMeshDisplayOnRepair)
        {
            if (_objectMeshReference != null)
                _objectMeshReference.SetActive(false);
            if (_particuleEffect != null)
                _particuleEffect.SetActive(true);
        }
        else
        {
            if (_objectMeshReference != null)
                _objectMeshReference.SetActive(true);
            if (_particuleEffect != null)
                _particuleEffect.SetActive(false);
        }
    }
    #endregion

    #region FuelType
    void FuelTypeStart()
    {
        IsBreak = false;
        IsReplace = false;

        if (_objectMeshReference != null)
            _objectMeshReference.SetActive(true);
        if (_particuleEffect != null)
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