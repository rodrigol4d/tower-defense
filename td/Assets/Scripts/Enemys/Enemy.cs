using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private NavMeshAgent _navMesh;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private Enemy _myEnemyScript;
    [SerializeField]
    private EnemyMoviment _myMovimentScript;

    //My target direciona a mira das torres

    public GameObject MyTarget;
    public void Start()
    {
        _animator = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _myEnemyScript = GetComponent<Enemy>();
        _myMovimentScript = GetComponent<EnemyMoviment>();



    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LifeDamage(10);
        }
    }



    public void LifeDamage(int damageTaken)
    {
        Life -= damageTaken;

        if(Life <= 0)
        {
            Debug.Log("vida menor que zero");
            Instantiate(_soul, transform.position, Quaternion.identity);
            Death();


        }
    }
    private void Death()
    {
        StartCoroutine(DeathCoRoutine());

    }
    private IEnumerator DeathCoRoutine()
    {
        _animator.SetBool("IsDead", true);
        _navMesh.enabled = false;
        _collider.enabled = false;

        
        
        _myMovimentScript.enabled = false;
        _myEnemyScript.enabled = false;


        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
