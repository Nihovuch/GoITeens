using UnityEngine;
using UnityEngine.InputSystem;

public class HeadBob : MonoBehaviour
{
    public CharacterController controller;

    public float walkBobSpeed = 8f;
    public float walkBobAmount = 0.05f;

    public float sprintBobSpeed = 12f;
    public float sprintBobAmount = 0.09f;

    public float smoothAmount = 8f;


    private Vector3 startPosition;

    private float timer;


    void Start()
    {
        startPosition = transform.localPosition;
    }


    void Update()
    {
        if (controller == null)
            return;


        bool moving = controller.velocity.magnitude > 0.1f;
        bool grounded = controller.isGrounded;


        if (moving && grounded)
        {
            float speed = walkBobSpeed;
            float amount = walkBobAmount;


            if (Keyboard.current != null &&
                Keyboard.current.leftShiftKey.isPressed)
            {
                speed = sprintBobSpeed;
                amount = sprintBobAmount;
            }


            timer += Time.deltaTime * speed;


            float bobY = Mathf.Sin(timer) * amount;
            float bobX = Mathf.Cos(timer / 2) * amount;


            Vector3 targetPosition =
                startPosition +
                new Vector3(bobX, bobY, 0);


            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                targetPosition,
                smoothAmount * Time.deltaTime
            );
        }
        else
        {
            timer = 0;


            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startPosition,
                smoothAmount * Time.deltaTime
            );
        }
    }
}