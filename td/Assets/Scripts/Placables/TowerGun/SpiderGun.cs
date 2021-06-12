using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpiderGun : MonoBehaviour
{
    [Header("Atributtes")]
    [SerializeField]
    private int _bulletDamage = 20;
    [SerializeField]
    private int _laserDamage = 40;
    

    [SerializeField]
    private Transform _rotateBodyPart;
    [SerializeField]
    private float TowerRadius = 1f;
    [SerializeField]
    private float LookSpeed = 0.10f;
    [SerializeField]
    private LayerMask layerMask;
    private Quaternion _rotGoalHead;
    private Vector3 _headDirection;
    private Quaternion _rotGoalBody;
    private Vector3 _bodyDirection;




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
    private ParticleSystem _muzzleFlashBullet;
    [SerializeField]
    private ParticleSystem _muzzleFlashLaser;

    [SerializeField]
    private float _fireBulletRate = 1f; // 1 bullet for second

    private float _fireBulletCountdown = 0f; // Time before shoot (Usar com variavel "time before shotting")
    [SerializeField]
    private float _timeBeforeShottingBullet = 1f;
    [SerializeField]
    private float _fireLaserRate = 1f; // 1 bullet for second
    
    private float _fireLaserCountdown = 0f; // Time before shoot (Usar com variavel "time before shotting")
    [SerializeField]
    private float _timeBeforeShottingLaser = 2f;

    [Header("Animator")]
    [SerializeField]
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // GetClosestEnemy();
        GetDestinationClosestEnemy();
        ShotReload();

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
            BulletShot(enemyTarget.gameObject);

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
            //  Pega tamanho do objeto e divide por 2, torre sempre ira olhar para o meio do objeto
            // float height = enemyTarget.GetComponent<Collider>().bounds.size.y;
            // Debug.Log(height);
            // var enemyPosition = new Vector3(enemyTarget.transform.position.x, enemyTarget.transform.position.y + (height /2), enemyTarget.transform.position.z);

            BodyandHeadMoviment();

            BulletShot(enemyTarget.gameObject);
            LaserShot();

        }
    }

    private void BodyandHeadMoviment()
    {
        Vector3 directionToTarget = enemyTarget.transform.position - _rotateBodyPart.transform.position;
        float angle = Vector3.Angle(_rotateBodyPart.transform.forward, directionToTarget);

        _headDirection = (enemyTarget.transform.position - _rotateBodyPart.transform.position).normalized;
        _rotGoalHead = Quaternion.LookRotation(_headDirection);
        _rotateBodyPart.transform.rotation = Quaternion.Slerp(_rotateBodyPart.transform.rotation, _rotGoalHead, LookSpeed);

   
      // Rotaciona o corpo, melhorar a lógica
      //  _bodyDirection = (enemyTarget.transform.position - transform.position).normalized;
      //  _rotGoalBody = Quaternion.LookRotation(_headDirection);
      //  transform.rotation = Quaternion.Slerp(transform.rotation, _rotGoalBody, LookSpeed);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_rotateBodyPart.transform.position, TowerRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if(currentHitObject !=null)
        {
            Gizmos.DrawLine(_rotateBodyPart.transform.position, currentHitObject.transform.position);
        }
    }

    private void ShotReload() {

        if(_fireBulletCountdown > -2f)
        {
            _fireBulletCountdown -= Time.deltaTime;
        }

        if (_fireLaserCountdown > -2f)
        {
            _fireLaserCountdown -=  Time.deltaTime;
        }
    }

    private void BulletShot(GameObject enemy)
    {
        Vector3 directionToTarget = enemyTarget.transform.position - _rotateBodyPart.transform.position ;
        float angle = Vector3.Angle(_rotateBodyPart.transform.forward, directionToTarget);

       
        if(_fireBulletCountdown <= 0f && enemyTarget != null && angle < 20)
        {
            _animator.Play("robot-first-attack");
            _muzzleFlashBullet.Play();
            enemyTarget.GetComponent<EnemyTakeDamage>().Hit(_bulletDamage);
            _fireBulletCountdown = _timeBeforeShottingBullet / _fireBulletRate;
        }
        
    }

    private void LaserShot()
    {
        Vector3 directionToTarget = enemyTarget.transform.position - _rotateBodyPart.transform.position;
        float angle = Vector3.Angle(_rotateBodyPart.transform.forward, directionToTarget);

        if (_fireLaserCountdown <= 0f && enemyTarget != null && angle < 20)
        {
            _animator.Play("robot-laser-attack");
            _muzzleFlashLaser.Play();
            enemyTarget.GetComponent<EnemyTakeDamage>().Hit(_laserDamage);
            _fireLaserCountdown = _timeBeforeShottingLaser / _fireLaserRate;
        }

    }

  

}
