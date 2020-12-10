using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float controllerHeight;
    public float controllerCrouchingHeight;
    public float playerSpeed = 10f;
    public float playerJumpHeight = 4f;
    private float gravitationalForce = -20f;
    private CharacterController controller;

    Vector3 playerVelocity;
    Vector3 graviationalVelocity;

    GameManager gameManager;
    Health health;
    HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        health = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetMaskFill((float)health.health / (float)health.maxHealth);

        if (health.health <= 0){
            gameManager.EndGame();
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movementInput = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.P)){
            gameManager.PauseGame();
        }

        if (controller.isGrounded)
        {
            graviationalVelocity.y = gravitationalForce * Time.deltaTime;
            if(Input.GetButtonDown("Jump")){
                graviationalVelocity.y += Mathf.Sqrt(playerJumpHeight * -2f * gravitationalForce);
            }

            if(Input.GetKey(KeyCode.LeftShift) || controller.height == controllerCrouchingHeight && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hit, 2.5f)){
                playerVelocity = movementInput * playerSpeed * 0.5f;
                controller.height = controllerCrouchingHeight;
            }
            else{
                playerVelocity = movementInput * playerSpeed;
                controller.height = controllerHeight;
            }
            
        }
        else {
            graviationalVelocity.y += gravitationalForce * Time.deltaTime;
            if(graviationalVelocity.y < -100f){
                health.DamageHealth(health.maxHealth);
            }
        }
        controller.Move((playerVelocity + graviationalVelocity) * Time.deltaTime);
    }
}
