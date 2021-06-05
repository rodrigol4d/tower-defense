using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField]
    private int _enemysLeft=30;

    [SerializeField]
    private int layer = 0;
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
      //  Debug.Log(other.gameObject.layer);
      //  Debug.Log(1 << other.gameObject.layer == 1 << 0);
        if (1 << other.gameObject.layer == 1 << layer) {
            
            Destroy(other.gameObject);
            _enemysLeft--;
        } ;
      //  if (other.tag == "Enemy")
     //   {
      //      Debug.Log("On Trigger Entrou");
      //      
      //      Destroy(other.gameObject);

      //  }
       
    }
}
