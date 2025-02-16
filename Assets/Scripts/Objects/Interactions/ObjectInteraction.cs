using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : IInteraction
{
    public void OnAction(MonoBehaviour script, Hands hands)
    {
        var obj = script.transform.GetComponent<ObjectsComponents>();

        if (obj != null && hands.GetObjectInHand(0) == null)
        {
            obj.Grab(hands.GetHandObjectTransform(0));
            hands.SetObjectInHand(0, obj.gameObject);
        }
    }
}
