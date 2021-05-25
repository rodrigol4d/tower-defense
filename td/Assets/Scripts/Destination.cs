using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField]
    private int _enemysPassed=30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger Entrou");
        _enemysPassed--;
        Destroy(other.gameObject);
    }
}
