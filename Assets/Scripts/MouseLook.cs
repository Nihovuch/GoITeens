using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    public float mouseSensitivity = 300f;
    public float smoothSpeed = 10f;

    private float xRotation = 0f;

    private Vector2 currentMouseDelta;
    private Vector2 smoothMouseDelta;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        Look();
    }


    void Look()
    {
        if (Mouse.current == null)
            return;


        Vector2 mouseInput = Mouse.current.delta.ReadValue();


        currentMouseDelta = Vector2.Lerp(
            currentMouseDelta,
            mouseInput,
            smoothSpeed * Time.deltaTime
        );


        float mouseX = currentMouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = currentMouseDelta.y * mouseSensitivity * Time.deltaTime;


        // Вертикальний рух камери

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(
            xRotation,
            -90f,
            90f
        );


        transform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);


        // Горизонтальний рух гравця

        playerBody.Rotate(
            Vector3.up * mouseX
        );
    }
}