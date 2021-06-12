using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyMoviment : MonoBehaviour
{
    
    [Header("Parametros do Ai")]
    [SerializeField]
    private GameObject destination;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;



    [Header("Detecção do inimigo")]

    [SerializeField]
    private Collider[] _myEnemys;
    [SerializeField]
    private LayerMask _myEnemyLayer;
    [SerializeField]
    private Collider _enemyEngaged;
    [SerializeField]
    private float _myCheckRadius = 2.84f;
    [SerializeField]
    private float LookMyEnemySpeed = 0.05f;
    [SerializeField]
    private float _myEnemyStopDistance = 1.1f;
    


    Quaternion RotGoal;
    Vector3 Direction;

    [Header("Attack")]
    [SerializeField]
    private Transform _attackPoint;

    [SerializeField]
    private bool _isAttacking = false;
    [SerializeField]
    private float _moveAfterAttack = 1.5f;
    [SerializeField]
    private float _enemyAttackDistance = 1.1f;
    [SerializeField]
    private float _attackRadius;
    [SerializeField]
    private int _attackDamage = 20;



    // Start is called before the first frame update
    void Start()
    {
        destination = GameObject.Find("Destination");
        _animator = GetComponent<Animator>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // MovimentAnimation();
        CheckMyEnemyAroundOrGo();

  

    }
    private void CheckMyEnemyAroundOrGo() {
        
        Collider[] _myEnemys = Physics.OverlapSphere(transform.position, _myCheckRadius, _myEnemyLayer);
        

        if (_myEnemys.Length > 0)
        {
            _enemyEngaged = _myEnemys.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).First();

            if(_isAttacking == false)
            {
                Direction = (_enemyEngaged.transform.position - transform.position).normalized;
                RotGoal = Quaternion.LookRotation(Direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, RotGoal, LookMyEnemySpeed);
           //     _navMeshAgent.stoppingDistance = _myEnemyStopDistance;
                _navMeshAgent.SetDestination(_enemyEngaged.transform.position);
                AttackMyEnemy();
            }
   
        }
        else
        {
            _navMeshAgent.stoppingDistance = 0.5f;
            _navMeshAgent.SetDestination(destination.transform.position);
            Debug.Log(destination.transform.position);

        }

       
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector3.forward);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _myCheckRadius);
        
    }

    private void MovimentAnimation()
    {

        //Inicio Nao Mecher - Logicas Anteriores


      //  if (_animator!= null)
      //  {
      //      velocityZ = this._navMeshAgent.speed;

           // _animator.SetFloat("Velocity Z", velocityZ);
       // }

        //Fim Nao Mecher - Logicas Anteriores
    }

    private void AttackMyEnemy()
    {
        float enemyDistance = Vector3.Distance(transform.position, _enemyEngaged.transform.position);
        Vector3 directionToTarget = transform.position - _enemyEngaged.transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
       // Debug.Log(angle);

        RaycastHit hit;
        if (enemyDistance <= _enemyAttackDistance && (angle < 178 && angle > 170 ))
        {
            _animator.SetTrigger("Attack");
            StartCoroutine(DelayRotationTimeDuringAttack());
   

        }
        if(Physics.Raycast(transform.position,Vector3.forward,out hit,20f, _myEnemyLayer))
        {

            Debug.Log("hit" + hit.transform.name);
        }
        
        

    }
    private IEnumerator DelayRotationTimeDuringAttack()
    {
        _isAttacking = true;
        _navMeshAgent.velocity = Vector3.zero; //Faz com que a para fique mais realista
        _navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(_moveAfterAttack);
        _isAttacking = false;
        if(_navMeshAgent!= _navMeshAgent.enabled == false)
        {
            _navMeshAgent.isStopped = false;


        }
        

    }

    private void ObjectDamage()
    {

        Collider[] hitEnemys = Physics.OverlapSphere(_attackPoint.transform.position, _attackRadius, _myEnemyLayer);

        if(hitEnemys != null)
        {
            foreach(var enemy in hitEnemys)
            {
                Debug.Log("we hit" + enemy.name);
                enemy.GetComponent<Player>().LifeDamage(_attackDamage);
            }
            

        }

    }

    
}
