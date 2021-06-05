using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IEnemy
{
    [SerializeField]
    private int Life = 100;
    [SerializeField]
    private GameObject Weapon;
    [SerializeField]
    private string _enemyName;
    [SerializeField]
    private Souls _soul;

    //My target direciona a mira das torres

    public GameObject MyTarget;



   public void LifeDamage(int damageTaken)
    {
        Life -= damageTaken;

        if(Life <= 0)
        {
            Debug.Log("vida menor que zero");
            Instantiate(_soul, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
