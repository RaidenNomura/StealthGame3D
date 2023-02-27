using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    #region Exposed

    [SerializeField] Camera cam;
    [SerializeField] float xSensitivity = 30f;
    [SerializeField] float ySensitivity = 30f;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    #endregion

    #region Methods

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    #endregion

    #region Private & Protected

    private float xRotation = 0f;

    #endregion
}
