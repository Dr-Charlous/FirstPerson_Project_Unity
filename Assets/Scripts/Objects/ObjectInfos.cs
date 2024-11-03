using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects",menuName = "Objetcs")]
public class ObjectInfos : ScriptableObject
{
    public int Damage;
    public int Munitions;
    public float Speed;
    public WeaponType Type;

    public enum WeaponType
    {
        Sword,
        Gun
    }
}
