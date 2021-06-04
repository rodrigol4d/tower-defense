using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moviment : MonoBehaviour
{
    private Transform destination;
    private NavMeshAgent navMeshAgent;
    public Animator animator;
   // float velocityX = 0.0f;
    float velocityZ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        destination = GameObject.Find("Destination").GetComponent<Transform>();


        navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.navMeshAgent.SetDestination(destination.transform.position);


    }

    // Update is called once per frame
    void Update()
    {
        MovimentAnimation();



    }

    private void MovimentAnimation()
    {
        

        if (animator!= null)
        {
            velocityZ = this.navMeshAgent.speed;

            animator.SetFloat("Velocity Z", velocityZ);
        }
    }
}
