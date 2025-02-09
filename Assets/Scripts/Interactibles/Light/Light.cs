using System.Collections;
using UnityEngine;

public class LightComponent : MonoBehaviour
{
    public bool IsActive = true;

    [SerializeField] Light _light;
    [SerializeField] Color _lightColor;
    [SerializeField] float _intensity;
    [SerializeField] float _range;

    [SerializeField] float _timeDuration = 1;
    [SerializeField] float _magnitudeLoss = 2;

    float _timer = 0;
    float _time = 0;

    private void Start()
    {
        _timer = Random.Range(30, 60);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(BuggingLight(_timeDuration, _magnitudeLoss));
        //}

        if (!GameManager.Instance.IsGamePause)
        {
            if (GameManager.Instance.PanelControl.Power.IsActive && GameManager.Instance.PanelControl.PowerLights.IsActive && !GameManager.Instance.Fuses[0].IsBreak)
            {
                if (!IsActive)
                {
                    LightSwitch();
                    IsActive = true;
                }

                if (_time >= _timer)
                {
                    StartCoroutine(BuggingLight(_timeDuration, _magnitudeLoss));
                    _timer = Random.Range(30, 60);
                    _time = 0;
                }
                else
                    _time += Time.deltaTime;
            }
            else
            {
                if (IsActive)
                {
                    _time = 0;
                    LightSwitch();
                    IsActive = false;
                }
            }
        }
    }

    public void LightSwitch()
    {
        if (IsActive)
            StartCoroutine(BuggingLight(0.5f, _magnitudeLoss, _intensity / 4));
        else
            StartCoroutine(BuggingLight(0.5f, _magnitudeLoss, _intensity));
    }

    public IEnumerator BuggingLight(float duration, float magnitude)
    {
        float originalIntensity = _intensity;
        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(_intensity / magnitude, _intensity);

            _light.intensity = x;
            elapsed += Time.deltaTime;
            yield return null;
        }

        _light.intensity = originalIntensity;
    }

    public IEnumerator BuggingLight(float duration, float magnitude, float offValue)
    {
        float originalIntensity = _intensity;
        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(_intensity / magnitude, _intensity);

            _light.intensity = x;
            elapsed += Time.deltaTime;
            yield return null;
        }

        _light.intensity = offValue;
    }
}
