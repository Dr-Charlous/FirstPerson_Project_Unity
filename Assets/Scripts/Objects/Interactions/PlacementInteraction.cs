using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementInteraction : IInteraction
{
    public void OnAction(MonoBehaviour script, Hands hands)
    {
        var placement = script.transform.GetComponent<ObjectPlacement>();

        if (placement != null && hands.GetObjectInHand(0) != null)
        {
            var objComp = hands.GetObjectInHand(0).GetComponent<ObjectsComponents>();

            if (!placement.IsReplace && objComp.ObjectInfos.Type == ObjectInfos.ObjectType.Change && objComp.ObjectInfos.SubType == placement.SubType)
            {
                placement.IsReplace = objComp.Use();
                hands.DestroyObjectInHand(0);

                placement.Repair();
            }
            else
                Debug.Log("Not the right one or not brake yet");
        }
    }
}
