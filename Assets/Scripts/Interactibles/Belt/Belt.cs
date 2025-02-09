using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField] float _force = 1;

    private void OnTriggerStay(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = transform.position - rb.transform.position;
            direction = new Vector3(direction.x, 0, direction.z);
            rb.AddForce(direction * _force, ForceMode.Force);
        }
    }
}