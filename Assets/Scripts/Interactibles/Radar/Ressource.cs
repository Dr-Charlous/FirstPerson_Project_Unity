using UnityEngine;

public class Ressource : MonoBehaviour
{
    public RadarMining.RessourcesType Type;
    public GameObject Mesh;
    public float RessourcesAmount = 10;
    public float DistanceShow = 1;
    public bool IsShow = true;

    private void Start()
    {
        Mesh.SetActive(false);
        RessourcesAmount = (transform.localScale.x + transform.localScale.y) * RessourcesAmount;
    }

    private void Update()
    {
        if (IsShow)
        {
            if ((GameManager.Instance.Mining.transform.position - transform.position).magnitude < DistanceShow && !Mesh.activeSelf)
                Mesh.SetActive(true);
            else if ((GameManager.Instance.Mining.transform.position - transform.position).magnitude > DistanceShow && Mesh.activeSelf)
                Mesh.SetActive(false);
        }
    }
}
