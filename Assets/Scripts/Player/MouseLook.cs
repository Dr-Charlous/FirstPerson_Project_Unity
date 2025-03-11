using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Values :")]
    public Transform CamTargetPosOnHead;
    public Transform CamTargetPos;

    [Range(0, 200)]
    public float MouseSensitivity = 100f;

    [Range(0f, 0.5f)]
    [Tooltip("When changing pos")]
    public float CamSpeedSensitivity = 0.1f;

    public bool IsOnHead = true;

    [SerializeField] Transform _playerBody;

    float _xRotation = 0f;

    void Start()
    {
        _xRotation = transform.localRotation.x;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePause)
            return;

        if (PlayerComponentManager.Instance.Stats.IsDead)
            return;

        if (IsOnHead)
        {
            float mouseX = PlayerComponentManager.Instance.PlayerInputs.Player.Look.ReadValue<Vector2>().x * MouseSensitivity / 10 * Time.deltaTime;
            float mouseY = PlayerComponentManager.Instance.PlayerInputs.Player.Look.ReadValue<Vector2>().y * MouseSensitivity / 10 * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);

            transform.position = Vector3.Lerp(transform.position, CamTargetPosOnHead.position, CamSpeedSensitivity);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, CamTargetPos.position, CamSpeedSensitivity);
            transform.rotation = Quaternion.Lerp(transform.rotation, CamTargetPos.rotation, CamSpeedSensitivity);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //Debug see front object if pb
    //private void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
    //    {
    //        Debug.Log(hit.transform.name);
    //    }
    //}
}
