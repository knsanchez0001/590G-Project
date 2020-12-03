using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 10f;
    private float playerJumpHeight = 4f;
    private float gravitationalForce = -20f;
    private CharacterController controller;

    Vector3 playerVelocity;
    Vector3 graviationalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movementInput = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            graviationalVelocity.y = gravitationalForce * Time.deltaTime;
            if(Input.GetButtonDown("Jump")){
                graviationalVelocity.y += Mathf.Sqrt(playerJumpHeight * -2f * gravitationalForce);
            }

            if(Input.GetKey(KeyCode.LeftShift) || controller.height == 2 && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hit, 2.5f)){
                playerVelocity = movementInput * playerSpeed * 0.5f;
                controller.height = 2f;
            }
            else{
                playerVelocity = movementInput * playerSpeed;
                controller.height = 3.8f;
            }
            
        }
        else {
            graviationalVelocity.y += gravitationalForce * Time.deltaTime;
        }
        controller.Move((playerVelocity + graviationalVelocity) * Time.deltaTime);
    }
}
