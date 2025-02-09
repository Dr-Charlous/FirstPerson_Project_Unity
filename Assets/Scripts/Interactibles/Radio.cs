using UnityEditor.Rendering;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public GameObject ActiveObjectInteract;
    public bool IsActive = false;

    [SerializeField] Dialogs _dialog;
    [SerializeField] Vector3 _openPos;
    [SerializeField] Vector3 _openRot;
    [SerializeField] Vector3 _closePos;
    [SerializeField] Vector3 _closeRot;
    [SerializeField] float _speed;
    Vector3 _actualTargetPos;
    Vector3 _actualTargetRot;

    private void Start()
    {
        if (IsActive)
        {
            _actualTargetPos = _openPos;
            _actualTargetRot = _openRot;
        }
        else
        {
            _actualTargetPos = _closePos;
            _actualTargetRot = _closeRot;
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
        transform.localPosition = Vector3.Lerp(transform.localPosition, _actualTargetPos, _speed);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_actualTargetRot), _speed);
    }


    public void ChangeTarget()
    {
        if (_actualTargetPos != _openPos)
        {
            ChangeTarget(true);
        }
        else
        {
            ChangeTarget(false);
        }
    }

    public void ChangeTarget(bool value)
    {
        if (value)
        {
            GameManager.Instance.Dialogue.UpdateDialogue(_dialog);
            _actualTargetPos = _openPos;
            _actualTargetRot = _openRot;
            IsActive = true;
        }
        else
        {
            _actualTargetPos = _closePos;
            _actualTargetRot = _closeRot;
            IsActive = false;
        }
    }
}
