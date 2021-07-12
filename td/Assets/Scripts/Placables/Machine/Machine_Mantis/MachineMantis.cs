using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MachineMantis : Placable
{
    [SerializeField]
    private Light[] _eyeLights;
    [Header("Damage")]
    private int _damage = 40;


    //[Header("Attack Atributes")]


    [Header("Enemy")]
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Collider[] hitColliders;
    [SerializeField]
    private Collider closestTarget;
    [SerializeField]
    private Collider enemyTarget;



    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetClosestEnemy();
        

    }
    private void Update()
    {
        StateStatus();
        DetectedStatus();
        LevelAtributtes();

    }

    public void GetClosestEnemy()
    {

        hitColliders = Physics.OverlapSphere(transform.position, _placebleRadius, layerMask);

        if (hitColliders.Length > 0)
        {
            closestTarget = hitColliders.OrderBy(x => (transform.position - x.transform.position).sqrMagnitude).First();

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
            transform.LookAt(enemyTarget.transform.gameObject.transform);
            BodyandHeadMoviment();

            if (BodyandHeadMoviment() <= 5)
            {
                _status = Status.engaged;
                //  BulletShot();

            }
            else
            {
                _status = Status.enemyDetected;
            }
          

        }

      



    }


   

    private float BodyandHeadMoviment()
    {
        Vector3 DirectionBodyToTarget = enemyTarget.transform.position - transform.position;
        float bodyAngle = Vector3.Angle(transform.forward, DirectionBodyToTarget);

        if (bodyAngle > 5)
        {
            _animator.SetBool("isTurning", true);
            Quaternion lookBodyRotation = Quaternion.LookRotation(DirectionBodyToTarget);
            Vector3 bodyRotation = Quaternion.Lerp(transform.rotation, lookBodyRotation, Time.deltaTime * _lookSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, bodyRotation.y, 0f);
        }
        Debug.Log(bodyAngle);
        return bodyAngle;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _placebleRadius);
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
        if (_status == Status.idle)
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

    private void LevelAtributtes()
    {
        switch (_level)
        {
            case PlacableLevel.level_1:
                _damage = 30;
                break;

            case PlacableLevel.level_2:
                //_canUseHeadShoot = true;
                _damage = 30 * 2;

                break;

            case PlacableLevel.level_3:
                _damage = 20 * 3;
               
                break;
        }
    }

}
