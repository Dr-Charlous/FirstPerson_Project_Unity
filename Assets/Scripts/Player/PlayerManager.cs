using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton && Input
    public static PlayerManager Instance;
    public CharaControl PlayerInputs;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("2 PlayerComponentManagers");

        PlayerInputs = new();
    }

    private void OnEnable()
    {
        PlayerInputs.Enable();
    }

    private void OnDisable()
    {
        PlayerInputs.Disable();
    }
    #endregion

    public PlayerMovement Movement;
    public Hands Hands;

    public MouseLook Look;
    public CameraShake Shake;

    public Interactions Interactions;
    public PlayerStats Stats;
    public UiManager UiManager;
}
