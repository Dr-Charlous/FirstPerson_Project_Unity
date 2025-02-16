using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnase : MonoBehaviour
{
    public FurnaseInteraction FurnaseInt = new FurnaseInteraction();
    public float TimerTarget = 0;
    public bool IsInfinit = false;
    public bool IsBreak = false;

    [SerializeField] GameObject _particuleEffect;

    float _timer = 0;

    private void Start()
    {
        IsBreak = false;

        if (TimerTarget > 0)
            _particuleEffect.SetActive(true);
        else
            _particuleEffect.SetActive(false);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePause && !IsInfinit)
        {
            if (_timer >= TimerTarget)
            {
                IsBreak = true;
                _particuleEffect.SetActive(false);
            }
            else
                _timer += Time.deltaTime;
        }
    }

    public void Repair(Fuel fuel)
    {
        if (IsBreak)
        {
            TimerTarget = fuel.FuelEnergyTime;
            _particuleEffect.SetActive(true);
        }
        else
        {
            TimerTarget -= _timer;
            TimerTarget += fuel.FuelEnergyTime;
        }

        _timer = 0;
        IsBreak = false;
    }
}
