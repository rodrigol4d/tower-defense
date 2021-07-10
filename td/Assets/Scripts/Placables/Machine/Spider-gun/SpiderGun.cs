using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpiderGun : Placable
{
    [SerializeField]
    private Light[] _eyeLights;
    [Header("Damage")]
    [SerializeField]
    private int _bulletDamage = 20;
    [SerializeField]
    private int _laserDamage = 40;

    [Header("Shot Atributes")]
    [SerializeField]
    private ParticleSystem _muzzleFlashBullet;
    [SerializeField]
    private ParticleSystem _muzzleFlashLaser;
    [SerializeField]
    private float _fireBulletRate = 1f; // 1 bullet for second
    [SerializeField]
    private float _timeBeforeShottingBullet = 1f;
    private float _fireBulletCountdown = 0f; // Time before shoot (Usar com variavel "time before shotting")
    [SerializeField]
    private float _fireLaserRate = 1f; // 1 bullet for second
    [SerializeField]
    private float _timeBeforeShottingLaser = 2f;
    private float _fireLaserCountdown = 0f; // Time before shoot (Usar com variavel "time before shotting")

    [Header("Enemy")]
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Collider[] hitColliders;
    [SerializeField]
    private Collider closestTarget;
    [SerializeField]
    private Collider enemyTarget;

    // Body Moviment
    [SerializeField]
    private Transform _rotateTopPart;

  


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
    private void Update()
    {
        StateStatus();
        DetectedStatus();


    }

    public void GetClosestEnemy()
    {

        hitColliders = Physics.OverlapSphere(transform.position, _placebleRadius, layerMask);

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
            BulletShot();

        }



    }

    public void GetDestinationClosestEnemy() {
        var destination = GameObject.Find("Destination");

        hitColliders = Physics.OverlapSphere(_rotateTopPart.transform.position, _placebleRadius, layerMask);
     
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

            if (BodyandHeadMoviment() <= 73)
            {
                _status = Status.engaged;
                BulletShot();
                LaserShot();
            }
            else
            {
                _status = Status.enemyDetected;
            }

        }
    }

    private float BodyandHeadMoviment()
    {
        Vector3 DirectionTopToTarget = enemyTarget.transform.position - _rotateTopPart.transform.position;
        float topAngle = Vector3.Angle(transform.forward, DirectionTopToTarget);
        Vector3 DirectionBodyToTarget = enemyTarget.transform.position - transform.position;
        float bodyAngle = Vector3.Angle(transform.forward, DirectionBodyToTarget);

        if (bodyAngle > 70)
        {
            _animator.SetBool("isTurning", true);
            Quaternion lookBodyRotation = Quaternion.LookRotation(DirectionBodyToTarget);
            Vector3 bodyRotation = Quaternion.Lerp(transform.rotation, lookBodyRotation, Time.deltaTime * _lookSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, bodyRotation.y, 0f);
            
            
        }
        if (bodyAngle <= 70)
        {
            _animator.SetBool("isTurning", false);
            Quaternion lookRotation = Quaternion.LookRotation(DirectionTopToTarget);
            Vector3 rotation = Quaternion.Lerp(_rotateTopPart.rotation, lookRotation, Time.deltaTime * _lookSpeed).eulerAngles;
            _rotateTopPart.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);   
        }
  
        Debug.Log(topAngle);

        return topAngle;


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_rotateTopPart.transform.position, _placebleRadius);
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

    private void BulletShot()
    {
      
        if(_fireBulletCountdown <= 0f && enemyTarget != null)
        {
            _animator.Play("spider-gun-attack");
            //_animator.Play("spider-gun-attack");
            _muzzleFlashBullet.Play();
            enemyTarget.GetComponent<EnemyTakeDamage>().Hit(_bulletDamage);
            _fireBulletCountdown = _timeBeforeShottingBullet / _fireBulletRate;
        }

        
    }

    private void LaserShot()
    {
    
        if (_fireLaserCountdown <= 0f && enemyTarget != null)
        {
            _animator.Play("spider-gun-head-attack");
            //_animator.Play("spider-gun-head-attack");
            _muzzleFlashLaser.Play();
            enemyTarget.GetComponent<EnemyTakeDamage>().Hit(_laserDamage);
            _fireLaserCountdown = _timeBeforeShottingLaser / _fireLaserRate;
        }
    
    }

    // Set Status based on condition

    private void DetectedStatus()
    {
        if (enemyTarget == null)
        {
            _status = Status.idle;
        }

        if (enemyTarget != null && _status != Status.engaged)
        {
            _status = Status.enemyDetected;

        }
        else if (_status == Status.engaged)
        {
            // GetDestinationClosestEnemy aplicando o engaged

        }
       




    }

    // Apply something based on status
    private void StateStatus()
    {
        if(_status == Status.idle)
        {
            _eyeLights.All(eye =>
            {
                if (eye.name == "GreenLight")
                {
                    eye.enabled = true;
                }
                else
                {
                    eye.enabled = false;
                }
                return true;
            });
        }
        if (_status == Status.enemyDetected)
        {
            _eyeLights.All(eye =>
            {
                if (eye.name == "RedLight")
                {
                    eye.enabled = true;

                }
                else
                {
                    eye.enabled = false;

                }
                return true;
            });

        }
        if (_status == Status.engaged)
        {
            _eyeLights.All(eye =>
            {
                if (eye.name == "RedLight")
                {
                    eye.enabled = true;

                }
                else
                {
                    eye.enabled = false;

                }
                return true;
            });
        }


    }


   



  

}
