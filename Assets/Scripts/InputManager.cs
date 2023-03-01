using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Exposed



    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _displacement = _playerInput.Player;
        _motor = GetComponent<PlayerMotor>();
        _displacement.Jump.performed += ctx => _motor.Jump();
        _displacement.Sneak.performed += ctx => _motor.Sneak();
        _displacement.Sprint.performed += ctx => _motor.Sprint();
    }

    private void FixedUpdate()
    {
        _motor.ProcessMove(_displacement.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _displacement.Enable();
    }

    private void OnDisable()
    {
        _displacement.Disable();
    }

    #endregion

    #region Methods



    #endregion

    #region Private & Protected

    private PlayerInputActions _playerInput;
    private PlayerInputActions.PlayerActions _displacement;
    private PlayerMotor _motor;

    #endregion
}
