using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("2 GameManagers");

        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
    #endregion

    public PlayerComponentManager Player;
    public CameraShake Shaker;
    public UiManager Ui;
    public DialogueManager Dialogue;
    [Header("")]
    public LightManager Lights;
    public RadarMining Mining;
    public ConveyorBelt Belt;
    public Furnase Furnase;
    public Radio Radio;
    public ObjectPlacement[] Fuses;
    public VehiculePanelManager PanelControl;
    [Header("")]
    public bool IsGamePause = false;
}
