using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayer : MonoBehaviour
{
    [SerializeField] Image BloodScreen;
    [SerializeField] Image DeathScreen;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePause)
        {
            BloodScreen.color = new Color(BloodScreen.color.r, BloodScreen.color.g, BloodScreen.color.b, 1-GameManager.Instance.Player.Stats.GetLife());
        }
    }

    public void Dead()
    {
        DeathScreen.gameObject.SetActive(true);
    }
}
