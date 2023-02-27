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
        playerInput = new PlayerInputActions();
        displacement = playerInput.Player;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        displacement.Jump.performed += ctx => motor.Jump();
    }

    private void FixedUpdate()
    {
        motor.ProcessMove(displacement.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(displacement.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        displacement.Enable();
    }

    private void OnDisable()
    {
        displacement.Disable();
    }

    #endregion

    #region Methods



    #endregion

    #region Private & Protected

    private PlayerInputActions playerInput;
    private PlayerInputActions.PlayerActions displacement;
    private PlayerMotor motor;
    private PlayerLook look;

    #endregion
}
