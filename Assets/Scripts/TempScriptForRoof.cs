using UnityEngine;

public class TempScriptForRoof : MonoBehaviour
{
    [SerializeField] GameObject[] ToActivate;

    void Start()
    {
        foreach (var item in ToActivate)
        {
            item.SetActive(true);
        }
    }
}
