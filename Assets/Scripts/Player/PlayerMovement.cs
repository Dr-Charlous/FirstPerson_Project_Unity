using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving = false;
    public bool IsRunning = false;
    public bool IsParalysed = false;

    [Header("Froces :")]
    [SerializeField] CharacterController _controller;

    [Tooltip("Walking speed")]
    [SerializeField] float _speed = 12f;

    [Tooltip("Running speed")]
    [SerializeField] float _runSpeed = 3f;

    [Tooltip("Gravity / fall speed")]
    [SerializeField] float _gravity = -15; //-9.81f;

    [Tooltip("Jump force")]
    [SerializeField] float _jumpForce = 3f;

    [Header("Ground :")]
    [Tooltip("Radius check")]
    [SerializeField] float _groundDistance = 0.4f;

    [Tooltip("Position to check if grounded")]
    [SerializeField] Transform _groundCheck;

    [Tooltip("Layer of ground checking")]
    [SerializeField] LayerMask _groundMask;

    Vector3 _velocity;
    bool _isGrounded;
    [SerializeField] Animator _animator;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePause)
            return;

        if (PlayerComponentManager.Instance.Stats.IsDead || IsParalysed)
            return;

        //Ground check
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        //Move
        float x = PlayerComponentManager.Instance.PlayerInputs.Player.Move.ReadValue<Vector2>().x;
        float z = PlayerComponentManager.Instance.PlayerInputs.Player.Move.ReadValue<Vector2>().y;

        Vector3 move = transform.right * x + transform.forward * z;

        if (move.magnitude > 0)
            IsMoving = true;
        else
            IsMoving = false;

        _animator.SetBool("IsWalking", IsMoving);

        //Run
        if (PlayerComponentManager.Instance.PlayerInputs.Player.Run.ReadValue<float>() != 0)
        {
            _controller.Move(move * (_speed + _runSpeed) * Time.deltaTime);
            IsRunning = true;
        }
        else
        {
            _controller.Move(move * _speed * Time.deltaTime);
            IsRunning = false;
        }

        _animator.SetBool("IsRunning", IsRunning);

        //Jump
        if (PlayerComponentManager.Instance.PlayerInputs.Player.Jump.ReadValue<float>() != 0 && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
            _animator.SetTrigger("IsJumping");
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundDistance);
    }
}
