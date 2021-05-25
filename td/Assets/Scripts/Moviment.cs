using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moviment : MonoBehaviour
{
    private Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        destination = GameObject.Find("Destination").GetComponent<Transform>();
       
       
        this.GetComponent<NavMeshAgent>().SetDestination(destination.transform.position);


    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
