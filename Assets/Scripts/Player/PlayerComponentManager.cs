using UnityEngine;

public class PlayerComponentManager : MonoBehaviour
{
    #region Singleton
    public static PlayerComponentManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("2 PlayerComponentManagers");
    }
    #endregion

    public PlayerMovement Movement;
    public Hands Hands;

    public MouseLook Look;
    public CharacterAnimation Animation;
    public CameraShake Shake;

    public Interactions Interactions;
    public PlayerStats Stats;
}
