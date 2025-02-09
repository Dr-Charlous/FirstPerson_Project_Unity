using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float LifeMax = 10;

    public float Life = 0;
    public float Air = 3;
    public bool IsDead = false;

    private void Start()
    {
        Life = LifeMax;
    }

    public void CheckDeath()
    {
        if (Air <= 0)
            Life -= Time.deltaTime;

        if (Life <= 0)
        {
            GameManager.Instance.Ui.UiPlayer.Dead();
            IsDead = true;
        }
    }

    public float GetLife()
    {
        return Life / LifeMax;
    }
}
