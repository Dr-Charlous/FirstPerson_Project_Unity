using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMove;
    [SerializeField] Transform _headTransform;
    [SerializeField] Vector3 _headUp;
    [SerializeField] Vector3 _headDown;
    [SerializeField] Vector3 _headTarget;
    [SerializeField] float _valueLerp;
    [SerializeField] float _valueSpeed;

    float _timer;
    float _speed;

    private void Start()
    {
        _speed = _valueSpeed;
        _timer = _speed;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePause || PlayerComponentManager.Instance.Stats.IsDead)
            return;

        if (_timer >= 0)
            _timer -= Time.deltaTime;
        else
        {
            Check();
            _timer = _speed;
        }

        if (_playerMove.IsMoving)
        {
            if (_playerMove.IsRunning)
                _speed = _valueSpeed / 2;
            else
                _speed = _valueSpeed;

            _headTransform.localPosition = Vector3.Lerp(_headTransform.localPosition, _headTarget, _valueLerp);
        }
        else
            _headTransform.localPosition = Vector3.Lerp(_headTransform.localPosition, _headDown, 0.01f);
    }

    void Check()
    {
        if (_headTarget != _headDown)
            _headTarget = _headDown;
        else
            _headTarget = _headUp;
    }
}
