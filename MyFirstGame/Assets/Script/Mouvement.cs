using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float JumpHeight = 3f;
    private float gravity = Physics.gravity.y;

    private CharacterController character;
    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask ground;
    private bool isGrounded;

    void Start()
    {
        character = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);

        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;

        // Mouvement 

        float z = 0;
        float x = 0;

        if (Input.GetKey(KeyCode.Z))
            z += 1;

        if (Input.GetKey(KeyCode.S))
            z -= 1;

        if (Input.GetKey(KeyCode.Q))
            x -= 1;

        if (Input.GetKey(KeyCode.D))
            x += 1;
        Vector3 move = transform.right * x + transform.forward * z;
        character.Move(move * moveSpeed*Time.deltaTime);

        // Jump 

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y =  Mathf.Sqrt(JumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);

    }
}
