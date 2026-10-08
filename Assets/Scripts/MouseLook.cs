using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    public float mouseSensitivity = 300f;

    private float xRotation = 0f;

    private void Start()
    {
        // Блокуємо курсор у центрі екрана
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Завжди залишаємо курсор заблокованим
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Look();
    }

    private void Look()
    {
        if (Mouse.current == null)
        {
            return;
        }

        // Отримуємо рух миші
        Vector2 mouseInput =
            Mouse.current.delta.ReadValue();

        // Розраховуємо поворот камери
        float mouseX =
            mouseInput.x *
            mouseSensitivity *
            Time.deltaTime;

        float mouseY =
            mouseInput.y *
            mouseSensitivity *
            Time.deltaTime;

        // Поворот вгору/вниз
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(
            xRotation,
            -90f,
            90f
        );

        transform.localRotation =
            Quaternion.Euler(
                xRotation,
                0f,
                0f
            );

        // Поворот вліво/вправо
        playerBody.Rotate(
            Vector3.up * mouseX
        );
    }
}