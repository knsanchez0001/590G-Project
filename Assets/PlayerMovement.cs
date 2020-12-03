using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 8f;
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

            if(Input.GetKey(KeyCode.LeftShift) || transform.localScale.y == 1f && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hit, 1.5f)){
                playerVelocity = movementInput * playerSpeed * 0.5f;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else{
                playerVelocity = movementInput * playerSpeed;
                transform.localScale = new Vector3(1f, 1.5f, 1f);
            }
            
        }
        else {
            graviationalVelocity.y += gravitationalForce * Time.deltaTime;
        }
        controller.Move((playerVelocity + graviationalVelocity) * Time.deltaTime);
    }
}
