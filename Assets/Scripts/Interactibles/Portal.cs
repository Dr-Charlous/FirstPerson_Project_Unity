using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum ObjectType
    {
        Object,
        Belt,
        Other
    }

    public Transform Gate;

    [SerializeField] Portal _connectPortal;
    [SerializeField] ObjectType _objType;

    private void OnTriggerEnter(Collider other)
    {
        if (_objType == ObjectType.Object && other.GetComponent<ObjectsComponents>() != null)
            Teleport(other.transform);

        if (_objType == ObjectType.Belt && other.GetComponent<BeltRef>() != null)
            Teleport(other.transform);

        if (_objType == ObjectType.Other)
            Teleport(other.transform);
    }

    void Teleport(Transform tf)
    {
        if (_connectPortal == null)
            Destroy(tf.gameObject);
        else
        {
            tf.transform.position += _connectPortal.Gate.position - Gate.position;
            tf.transform.rotation = Quaternion.Euler(tf.transform.rotation.eulerAngles + _connectPortal.Gate.rotation.eulerAngles - Gate.rotation.eulerAngles);
        }
    }
}
