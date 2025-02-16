using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : IInteraction
{
    public void OnAction(MonoBehaviour script, Hands hands)
    {
        var door = script.transform.GetComponent<Doors>();

        if (door != null)
        {
            //var objComp = Hands[0].ObjectInHand.GetComponent<ObjectsComponents>();

            //if (objComp != null && door.IsLocked)
            //    door.IsLocked = !objComp.Use();

            if (!door.IsLocked)
                door.ChangeTarget();
            else
                Debug.Log("Door close");
        }
    }
}
