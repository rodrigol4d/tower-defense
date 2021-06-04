using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public CharacterController controller;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float speed = 12f;

    Animator animator;
    Vector3 velocity;
    
    
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;


    public float jumpHeight = 3f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }


    // Update is called once per frame
    void Update()
    {
          Moviment();



    }

    public void Moviment()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Lógica para gravidade

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Lógica para pulo

        if (Input.GetButtonDown("Jump") && isGrounded ) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Logica para animacao
        
            velocityZ = move.z * speed;
            velocityX = move.x * speed;
       
        

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);


    }


}
