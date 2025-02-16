using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaseInteraction : IInteraction
{
    public void OnAction(MonoBehaviour script, Hands hands)
    {
        var furnase = script.transform.GetComponent<Furnase>();

        if (furnase != null && hands.GetObjectInHand(0) != null)
        {
            furnase.Repair(hands.GetObjectInHand(0).GetComponent<Fuel>());

            hands.DestroyObjectInHand(0);
        }
    }
}
