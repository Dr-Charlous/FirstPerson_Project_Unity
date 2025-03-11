using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Ui :")]
    public UiScreenControl ScreenControl;
    public UiPlayer UiPlayer;

    [SerializeField] Transform _uiMenu;
    [SerializeField] TextMeshProUGUI _uiTimer;
    [SerializeField] TextMeshProUGUI _uiText;
    [SerializeField] Slider _slider;

    float _time = 0;

    private void Start()
    {
        _uiMenu.gameObject.SetActive(false);
        _slider.value = PlayerComponentManager.Instance.Look.MouseSensitivity;
    }

    private void Update()
    {
        if (!PlayerComponentManager.Instance.Stats.IsDead)
        {
            if (!GameManager.Instance.IsGamePause)
            {
                _time += Time.deltaTime;
                _uiTimer.text = "Time : " + (int)_time;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _uiMenu.gameObject.SetActive(!_uiMenu.gameObject.activeSelf);
                GameManager.Instance.IsGamePause = _uiMenu.gameObject.activeSelf;

                if (Cursor.lockState != CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
            }

            if (_uiMenu.gameObject.activeSelf)
            {
                Ui();
            }
        }
        else
            Cursor.lockState = CursorLockMode.None;
    }

    void Ui()
    {
        PlayerComponentManager.Instance.Look.MouseSensitivity = _slider.value;
        _uiText.text = _slider.value + "/" + _slider.maxValue;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}