using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool IsLocked = false;
    public bool IsAutoClose = true;
    public DoorInteraction DoorInt = new DoorInteraction();

    [SerializeField] Transform _door;
    [SerializeField] Vector3 _openPos;
    [SerializeField] Vector3 _openRot;
    [SerializeField] Vector3 _closePos;
    [SerializeField] Vector3 _closeRot;
    [SerializeField] float _doorSpeed;
    [SerializeField] float _timeDoorStayOpen = 10f;

    Vector3 _actualTargetPos;
    Vector3 _actualTargetRot;
    float _timer = 0;
    bool _isDoorOpen = false;

    private void Start()
    {
        //_closePos = _door.localPosition;
        //_closeRot = _door.localRotation.eulerAngles;
        _actualTargetPos = _closePos;
        _actualTargetRot = _closeRot;
    }
    private void Update()
    {
        if (IsAutoClose)
        {
            if (_isDoorOpen && !GameManager.Instance.IsGamePause)
            {
                _timer += Time.deltaTime;

                if (_timer >= _timeDoorStayOpen)
                {
                    _timer = 0;
                    ChangeTarget();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        _door.localPosition = Vector3.Lerp(_door.localPosition, _actualTargetPos, _doorSpeed);
        _door.localRotation = Quaternion.Lerp(_door.localRotation, Quaternion.Euler(_actualTargetRot), _doorSpeed);
    }

    public void ChangeTarget()
    {
        _timer = 0;

        if (!IsLocked)
        {
            if (_actualTargetPos != _openPos)
            {
                _actualTargetPos = _openPos;
                _actualTargetRot = _openRot;
                _isDoorOpen = true;
            }
            else
            {
                _actualTargetPos = _closePos;
                _actualTargetRot = _closeRot;
                _isDoorOpen = false;
            }
        }
    }
}
