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

   public void LifeDamage(int damageTaken)
    {
        Life -= damageTaken;

        if(Life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
