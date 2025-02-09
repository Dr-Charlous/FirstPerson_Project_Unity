using System.Linq;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField] Transform _parent;
    [Range(0f, 1f)][SerializeField] float _rigidity = 0.5f;

    private void OnMouseDrag()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            Vector3 difference = hit.point - _parent.position;
            Quaternion rotation = Quaternion.LookRotation(difference, Vector3.down);
            rotation = Quaternion.Lerp(_parent.localRotation, rotation, _rigidity);
            _parent.localRotation = new Quaternion(_parent.localRotation.x, rotation.y, _parent.localRotation.z, rotation.w);
        }
    }
}
