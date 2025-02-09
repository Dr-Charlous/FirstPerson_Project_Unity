using UnityEngine;

public class VehiculePanelManager : MonoBehaviour
{
    public Vector3 Position = Vector3.zero;
    public Interactible Power;
    public Interactible PowerLights;
    public Interactible Drill;

    [SerializeField] Interactible _front;
    [SerializeField] Interactible _back;
    [SerializeField] Interactible _right;
    [SerializeField] Interactible _left;
    [SerializeField] float _speedMove;
    [SerializeField] float _speedRot;

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGamePause)
        {
            Vector3 direction = Vector3.zero;

            if (GameManager.Instance.Furnase.IsBreak)
                Power.ChangeTarget(false);

            if (Power.IsActive)
            {
                //StartCoroutine(GameManager.Instance.Shaker.Shake(0.1f, 0.01f));

                if (_front.IsActive)
                {
                    direction += Vector3.up;
                }
                if (_back.IsActive)
                {
                    direction += Vector3.down;
                }
                if (_right.IsActive)
                {
                    direction += Vector3.right;
                }
                if (_left.IsActive)
                {
                    direction += Vector3.left;
                }
            }

            if (direction != Vector3.zero)
            {
                var movement = direction.normalized * _speedMove * Time.deltaTime;
                var rotation = direction.normalized * _speedRot * Time.deltaTime;

                Position += movement;

                GameManager.Instance.Mining.Move(new Vector3(0, movement.y), Quaternion.Euler(new Vector3(0, 0, rotation.x)));
            }
        }
    }
}
