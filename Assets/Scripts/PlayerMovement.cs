using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    Vector3 moveDirection = Vector3.zero;
    Vector3 velocity;

    public float moveSpeed = 8f;
    public float jumpSpeed = 8f;
    public float gravity = -19.62f;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (characterController.isGrounded && velocity.y <0f)
        {
            // we don't need to apply gravity when on the floor, otherwise velocity will be decreasing
            // constantly and get faster as time passes.
            velocity.y = -2f;
        }

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            velocity.y = jumpSpeed;
        }

        moveDirection = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
