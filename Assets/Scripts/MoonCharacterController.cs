using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MoonCharacterController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Moon Physics")]
    public float gravity = -5f;
    public float jumpHeight = 15f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.25f;
    public LayerMask groundMask;

    [Header("Sprint")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;

    private float currentSpeed;


    private CharacterController controller;

    private Vector3 velocity;
    private Vector2 moveInput;

    private bool jumpPressed;
    private bool isGrounded;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
    }


    void Update()
    {
        GetInput();

        CheckGround();

        Move();
    }


    void GetInput()
    {
        moveInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                moveInput.y += 1;

            if (Keyboard.current.sKey.isPressed)
                moveInput.y -= 1;

            if (Keyboard.current.aKey.isPressed)
                moveInput.x -= 1;

            if (Keyboard.current.dKey.isPressed)
                moveInput.x += 1;


            jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        }

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }


    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundRadius,
            groundMask
        );


        if (isGrounded && velocity.y < 0)
        {
            // Гравець лишається на землі
            velocity.y = -5f;
        }
    }


    void Move()
    {
        // Горизонтальний рух

        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;


        controller.Move(
            move * currentSpeed * Time.deltaTime
        );


        // Стрибок

        if (jumpPressed && isGrounded)
        {
            velocity.y =
                Mathf.Sqrt(
                    jumpHeight * -2f * gravity
                );
        }


        // Гравітація

        velocity.y += gravity * Time.deltaTime;


        // Вертикальний рух

        controller.Move(
            velocity * Time.deltaTime
        );
    }


    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;


        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundRadius
        );
    }
}