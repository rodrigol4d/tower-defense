using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerHead : MonoBehaviour
{
    [Header("Atributtes")]

    public float TowerRadius = 1f;
    public int layerMask = 1 << 6;
    Quaternion RotGoal;
    Vector3 Direction;
    public float LookSpeed = 0.10f;


    [Header("Target")]

    public GameObject currentHitObject;
    [SerializeField]
    Collider[] hitColliders;
    [SerializeField]
    Collider closestTarget;
    [SerializeField]
    Collider enemyTarget;

    [Header("Weapon")]
    public TowerGun towerGun;


    [Header("Shot Atributes")]

    [SerializeField]
    public float FireRate = 1f; // 1 bullet for second
    [SerializeField]
    private float _fireCountdown = 0f; // Time before shoot


  

   



    // Start is called before the first frame update
    void Start()
    {
      






    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // GetClosestEnemy();
        GetDestinationClosestEnemy();
        ShotReload();






    }
    public void GetEnemyTest()
    {
       // if(Physics.SphereCast(transform.position,3f,Vector3.lo))
    }

    public void GetClosestEnemy()
    {

        hitColliders = Physics.OverlapSphere(transform.position, TowerRadius, layerMask);

        if (hitColliders.Length > 0) {
            closestTarget = hitColliders.OrderBy(x => (transform.position - x.transform.position).sqrMagnitude).First();

        }
        else
        {
            closestTarget = null;
            enemyTarget = null;
        }
    
        //Verica se existe inimigo dentro do array da torre
        if(enemyTarget == null )
        {
            enemyTarget = closestTarget;

        }
        else if(!hitColliders.Any(x => x == enemyTarget))
        {
            enemyTarget = closestTarget;

        }
       
        if(enemyTarget !=null)
        {
            transform.LookAt(enemyTarget.transform.gameObject.transform);
            Shot(enemyTarget.gameObject);

        }



    }

    public void GetDestinationClosestEnemy() {
        var destination = GameObject.Find("Destination");

        hitColliders = Physics.OverlapSphere(transform.position, TowerRadius, layerMask);

        if (hitColliders.Length > 0)
        {
            closestTarget = hitColliders.OrderBy(x => (destination.transform.position - x.transform.position).sqrMagnitude).First();

        }
        else
        {
            closestTarget = null;
            enemyTarget = null;
        }

        //Verica se existe inimigo dentro do array da torre
        if (enemyTarget == null)
        {
            enemyTarget = closestTarget;

        }
        else if (!hitColliders.Any(x => x == enemyTarget))
        {
            enemyTarget = closestTarget;

        }

        if (enemyTarget != null)
        {
            //    transform.LookAt(enemyTarget.transform.gameObject.transform);
            Direction = (enemyTarget.transform.position - transform.position).normalized;
            RotGoal = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, RotGoal, LookSpeed);

            Shot(enemyTarget.gameObject);

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TowerRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if(currentHitObject !=null)
        {
            Gizmos.DrawLine(transform.position, currentHitObject.transform.position);

        }
        

    }

    private void ShotReload() {

        if(_fireCountdown > -2f)
        {
            _fireCountdown -= Time.deltaTime;

        }
       
    }

    private void Shot(GameObject enemy)
    {
        if(_fireCountdown <= 0f)
        {
            towerGun.Shoot(enemy);
            _fireCountdown = 1f / FireRate;


        }
        
    }
}
