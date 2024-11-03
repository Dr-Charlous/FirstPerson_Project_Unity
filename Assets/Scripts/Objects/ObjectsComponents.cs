using UnityEngine;

public class ObjectsComponents : MonoBehaviour
{
    public ObjectInfos ObjectInfos;

    public virtual void Use()
    {
        if (ObjectInfos.Type == ObjectInfos.WeaponType.Sword)
        {
            Debug.Log("Sword attack");
        }
        else if (ObjectInfos.Type == ObjectInfos.WeaponType.Gun)
        {
            Debug.Log("Gun attack");
        }
    }
}
