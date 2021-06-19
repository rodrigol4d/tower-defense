using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   // public GameObject enemy;
    public GameObject enemy { get; set; }

    public int damage { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
     //   enemy = GameObject.Find("Human");



    }

    // Update is called once per frame
    void Update()
    {
        
        if(enemy != null)
        {
            Search();

        }
        else
        {
            Destroy(this.gameObject);
        }


    }

     public void Search()
    {
        //  Pega tamanho do objeto e divide por 2, bala sempre ira olhar para o meio do objeto

       // float height = enemy.GetComponent<Collider>().bounds.size.y;
        //var enemyPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y + (height / 2), enemy.transform.position.z);

        var GoalPosition = enemy.transform.position;
            var MyPosition = transform.position;
            var destination = (GoalPosition - MyPosition);
            transform.Translate(destination.normalized * 0.1f, Space.World);
 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
           

            other.GetComponent<Enemy>().LifeDamage(damage);
         
            Destroy(this.gameObject);

        }
        
    }
}
