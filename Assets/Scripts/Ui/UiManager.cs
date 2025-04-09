using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public bool IsGamePause = false;

    [SerializeField] Transform _uiMenu;
    [SerializeField] TextMeshProUGUI _uiTimer;
    [SerializeField] TextMeshProUGUI _uiText;
    [SerializeField] Slider _slider;

    float _time = 0;

    private void Start()
    {
        _uiMenu.gameObject.SetActive(false);
        _slider.maxValue = 200;
        _slider.value = PlayerManager.Instance.Look.MouseSensitivity;
    }

    private void Update()
    {
        PauseMenu();
    }

    void Ui()
    {
        PlayerManager.Instance.Look.MouseSensitivity = _slider.value;
        _uiText.text = _slider.value + "/" + _slider.maxValue;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void PauseMenu()
    {
        if (!PlayerManager.Instance.Stats.IsDead)
        {
            if (!IsGamePause)
            {
                _time += Time.deltaTime;
                _uiTimer.text = "Time : " + (int)_time + "s";
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _uiMenu.gameObject.SetActive(!_uiMenu.gameObject.activeSelf);
                IsGamePause = _uiMenu.gameObject.activeSelf;

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
}