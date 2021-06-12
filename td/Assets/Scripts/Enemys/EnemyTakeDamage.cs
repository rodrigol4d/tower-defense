using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    public enum collisionType { head,body,arms,legs}
    public collisionType damageType;
    public Enemy enemyData;
    

    public void Hit(int value)
    {
       
        
        try
        {
            switch (damageType)
            {
                case collisionType.head:
                    enemyData.LifeDamage(value * 2);
                    break;
                case collisionType.body:
                    enemyData.LifeDamage(value);
                    break;

                case collisionType.arms:
                    enemyData.LifeDamage(value / 2);
                    break;
                case collisionType.legs:
                    enemyData.LifeDamage(value / 2);
                    break;
            }
  
            

        }
        catch
        {
            print("Enemy is not connected");
        }

    }
    public void checkAlive() {
        if(enemyData.GetLife() <=0)
        {

        }
    }


    
}
