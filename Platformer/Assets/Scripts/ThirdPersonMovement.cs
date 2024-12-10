using UnityEngine;
using UnityEngine.Events;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;

    private float yVelocity = -0.1f;
    private bool isJumping = false; // Track if the player is jumping
    public float jumpForce = 5f;    // Force applied for jumping
    public int jumpCount = 0;       // Tracks the number of jumps performed
    public int jumpCountMax = 2;    // Maximum number of jumps (double jump)
    public float gravity = 9.81f;   // Use a more realistic gravity value
    public float turnSmoothTime = 0.1f;

    public Transform cam;

    public UnityEvent walkEvent, jumpEvent;

    private float turnSmoothVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction on the X and Z axis
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Rotate the character smoothly toward the movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the character in the desired direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move( moveDir .normalized* speed * Time.deltaTime);
            if (controller.isGrounded)
            {
                walkEvent.Invoke();
            }
        }

        // Handle jumping and gravity
        if (controller.isGrounded)
        {
            // Reset vertical velocity and jump count when grounded
            yVelocity = -0.1f;  // Small value to stick to the ground (not 0 to prevent floating)
            if (isJumping)
            {
                isJumping = false; // Reset jumping flag when grounded
            }

            jumpCount = 0; // Reset jump count when on the ground

            // Allow jumping if the player presses jump and hasn't exceeded the max jump count
            if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
            {
                jumpEvent.Invoke();
                yVelocity = jumpForce;  // Apply jump force
                isJumping = true;
                jumpCount++;  // Increment jump count
            }
        }
        else
        {
            // Apply gravity when in the air
            yVelocity -= gravity * Time.deltaTime;

            // Allow second jump (double jump) if in the air and hasn't exceeded max jumps
            if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
            {
                jumpEvent.Invoke();
                yVelocity = jumpForce;  // Apply jump force again
                isJumping = true;
                jumpCount++;  // Increment jump count for double jump
            }
        }
        
        // Apply the vertical velocity (y) to the movement direction
        controller.Move(new Vector3(0f, yVelocity, 0f) * Time.deltaTime);
    }
}