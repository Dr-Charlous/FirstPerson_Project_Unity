using UnityEngine;

public class Interactible : MonoBehaviour
{
    public GameObject ActiveObjectInteract;
    public bool IsActive = false;
    public bool IsActivableOut = false;

    [SerializeField] Vector3 _openRot;
    [SerializeField] Vector3 _closeRot;
    [SerializeField] Vector3 _actualTarget;
    [SerializeField] float _speed;

    private void Start()
    {
        if (IsActive)
        {
            _actualTarget = _openRot;
            IsActive = true;
        }
        else
        {
            _actualTarget = _closeRot;
            IsActive = false;
        }
    }

    private void Update()
    {
        if (ActiveObjectInteract != null)
        {
            if (IsActive)
            {
                ActiveObjectInteract.SetActive(true);
            }
            else
            {
                ActiveObjectInteract.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_actualTarget), _speed);
    }

    public void ChangeTarget()
    {
        if (_actualTarget != _openRot)
        {
            _actualTarget = _openRot;
            IsActive = true;
        }
        else
        {
            _actualTarget = _closeRot;
            IsActive = false;
        }
    }

    public void ChangeTarget(bool value)
    {
        if (value)
        {
            _actualTarget = _openRot;
            IsActive = true;
        }
        else
        {
            _actualTarget = _closeRot;
            IsActive = false;
        }
    }
}
