using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    #region Exposed

    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float jumpHeight = 3f;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
    }

    #endregion

    #region Methods

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
    }

    #endregion

    #region Private & Protected

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    #endregion
}
