using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float range = 100f;
    [SerializeField]
    private ParticleSystem _muzzleFlash;
    //[SerializeField]
    //private ParticleSystem _impactDemonParticle;
    [SerializeField ]
    private EnemyTakeDamage enemyDamage;

    public Camera fpsCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
          
        }
        
    }

    private void Shoot()
    {
        _muzzleFlash.Play();
        RaycastHit hit;
        



        if (Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit,range))
        {
            enemyDamage = hit.transform.GetComponent<EnemyTakeDamage>();
            Debug.Log(hit.transform.name);

            if(enemyDamage != null) {
                enemyDamage.Hit(damage);
            }

         //   Enemy enemyTarget = hit.transform.GetComponent<Enemy>();

         //   if(enemyTarget != null)
         //   {
         //       enemyTarget.LifeDamage(damage);
         //   }





     //       var ferida = Instantiate(_impactDemonParticle, hit.point, Quaternion.LookRotation(hit.normal));
     //       hit.rigidbody.AddForce(-hit.normal * 100);

     //       Destroy(ferida, 0.4f);
        }

      
    {

        
    }
}
}
