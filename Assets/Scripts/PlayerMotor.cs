using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    #region Exposed

    [SerializeField] Transform cam;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _sprint = 2f;
    [SerializeField] float _gravity = -9.8f;
    [SerializeField] float _jumpHeight = 3f;
    [SerializeField] float _turnSmoothTime = 0.1f;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                _animator.SetBool("isSneaking", true);
                //_controller.height = Mathf.Lerp(_controller.height, 1, p);
            }
            else
            {
                _animator.SetBool("isSneaking", false);
                //_controller.height = Mathf.Lerp(_controller.height, 2, p);
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    #endregion

    #region Methods

    public void ProcessMove(Vector2 input)
    {
        float horizontal = input.x;
        float vertical = input.y;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            _animator.SetBool("isJogging", true);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
        }
        else
            _animator.SetBool("isJogging", false);

        _playerVelocity.y += _gravity * Time.deltaTime;
        if (_isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = -2f;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _animator.SetTrigger("_jumping");
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3 * _gravity);
        }
    }

    public void Sneak()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        _sprinting = !_sprinting;
        if (_sprinting)
        {
            _speed += _sprint;
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _speed -= _sprint;
            _animator.SetBool("isRunning", false);
        }
    }

    #endregion

    #region Private & Protected

    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    private float _turnSmoothVelocity;

    private bool _sprinting;
    private bool crouching;
    private bool lerpCrouch;
    private float crouchTimer;

    private Animator _animator;

    #endregion
}
