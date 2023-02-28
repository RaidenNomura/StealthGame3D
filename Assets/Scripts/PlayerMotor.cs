using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    #region Exposed

    [SerializeField] Transform cam;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _sprint = 1f;
    [SerializeField] float _gravity = -9.8f;
    [SerializeField] float _jumpHeight = 3f;
    [SerializeField] float _turnSmoothTime = 0.1f;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _controller.isGrounded;
    }

    #endregion

    #region Methods

    public void ProcessMove(Vector2 input)
    {
        Vector3 direction = Vector3.zero;
        direction.x = input.x;
        direction.z = input.y;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _speed * _sprint * Time.deltaTime);
        }

        _playerVelocity.y += _gravity * Time.deltaTime;
        if (_isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = -2f;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (_isGrounded)
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3 * _gravity);
    }

    public void Sprint()
    {
        _sprint = 2;
    }

    public void NoSprint()
    {
        _sprint = 1;
    }

    #endregion

    #region Private & Protected

    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    private float _turnSmoothVelocity;

    #endregion
}
