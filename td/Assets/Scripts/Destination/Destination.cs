using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField]
    private int _enemysLeft=30;
    // Start is called before the first frame update
    public int GetEnemiesLeft()
    {
        return _enemysLeft;
    }
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
        _enemysLeft--;
        Destroy(other.gameObject);
    }
}
