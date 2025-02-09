using System;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] Hand[] _hands;
    [SerializeField] float _lerpDelay = 0.9f;

    private void Update()
    {
        for (int i = 0; i < _hands.Length; i++)
        {
            //_hands[i].ArmMeshTransform.position = Vector3.Lerp(_hands[i].ArmMeshTransform.position, transform.position, _lerpDelay);
            _hands[i].ArmMeshTransform.rotation = Quaternion.Lerp(_hands[i].ArmMeshTransform.rotation, transform.rotation, _lerpDelay);
        }
    }

    public void SetObjectInHand(int handNumber, GameObject obj)
    {
        _hands[handNumber].ObjectInHand = obj;
    }
    
    public GameObject GetObjectInHand(int handNumber)
    {
        return _hands[handNumber].ObjectInHand;
    }

    public void DestroyObjectInHand(int handNumber)
    {
        Destroy(_hands[handNumber].ObjectInHand);
        _hands[handNumber].ObjectInHand = null;
    }
}

[Serializable]
public class Hand
{
    public Transform HandTransformTarget;
    public Transform ArmMeshTransform;

    [Header("Object :")]
    public GameObject ObjectInHand = null;
    public Transform HandTransformObjectPlacement;
}