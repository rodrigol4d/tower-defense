using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPreFab;

    [SerializeField]
    private int _damage= 20;



    public void Shoot(GameObject enemy)
    {
        GameObject bulletInstantiated = Instantiate(bulletPreFab, transform.position, transform.rotation);
        Bullet bullet = bulletInstantiated.GetComponent<Bullet>();
        bullet.enemy = enemy;
        bullet.damage = _damage;

    }
   
}
