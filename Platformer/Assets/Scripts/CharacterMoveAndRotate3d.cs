using UnityEngine;

[CreateAssetMenu(menuName = "Character Patterns/Move And Rotate")]
public class CharacterMoveAndRotate3d : CharacterPattern
{
    public Vector3 rotateDirection;
    public float rotateSpeed = 10;

    private float yVelocity = 0f;  // Vertical velocity
    private bool isJumping = false; // Track if the player is jumping

    public override void Move(CharacterController controller)
    {
        // Handle horizontal movement
        positionDirection.Set(0, 0, Speed * Input.GetAxis(vAxis));

        // Handle rotation
        rotateDirection.y = rotateSpeed * Input.GetAxis(hAxis);
        controller.transform.Rotate(rotateDirection);

        // Transform direction to world space
        positionDirection = controller.transform.TransformDirection(positionDirection);

        // Jumping Logic (Smooth Gravity)
        if (controller.isGrounded)
        {
            // Reset vertical velocity when grounded
            if (isJumping)
            {
                yVelocity = -0.1f;  // Small negative value to stick to the ground
                isJumping = false;
            }

            // Reset jump count when player is grounded
            jumpCount = 0;  // Player can start fresh with jumping

            // If we are on the ground and pressing jump, apply the jump force
            if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
            {
                yVelocity = jumpForce;  // Apply the jump force
                isJumping = true;
                jumpCount++;  // Increment jump count on jump
            }
        }
        else
        {
            // Apply gravity when not grounded
            yVelocity -= gravity * Time.deltaTime;

            // If the player is in the air and has remaining jumps
            if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
            {
                // Perform second (or additional) jump
                yVelocity = jumpForce;  // Apply jump force again
                isJumping = true;
                jumpCount++;  // Increment jump count for double jump
            }
        }

        // Update vertical velocity (y direction)
        positionDirection.y = yVelocity;

        // Apply movement to the controller
        controller.Move(positionDirection * Time.deltaTime);
    }
}
