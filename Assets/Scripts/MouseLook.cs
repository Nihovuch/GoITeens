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

    private void Start()
    {
        // Блокує курсор у центрі екрана
        Cursor.lockState = CursorLockMode.Locked;

        // Ховає курсор
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();

        // Завжди залишає курсор заблокованим
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Look()
    {
        if (Mouse.current == null)
        {
            return;
        }

        Vector2 mouseInput = Mouse.current.delta.ReadValue();

        currentMouseDelta = Vector2.Lerp(
            currentMouseDelta,
            mouseInput,
            smoothSpeed * Time.deltaTime
        );

        float mouseX =
            currentMouseDelta.x *
            mouseSensitivity *
            Time.deltaTime;

        float mouseY =
            currentMouseDelta.y *
            mouseSensitivity *
            Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(
            xRotation,
            -90f,
            90f
        );

        transform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(
            Vector3.up * mouseX
        );
    }
}