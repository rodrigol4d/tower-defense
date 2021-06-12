using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public enum AiState
{
    alive,
    dead
}

public class Enemy : MonoBehaviour,IEnemy
{
    [SerializeField]
    private int _life = 100;
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
    [SerializeField]
    private Collider[] _hitBodyParts;
    [SerializeField]
    private AiState state;
    

    
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
        _life -= damageTaken;

        if(_life <= 0 && state != AiState.dead)
        {
            state = AiState.dead;
            Debug.Log("vida menor que zero");
            
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


        GetComponent<Collider>().enabled = false;
        foreach (Collider obj in _hitBodyParts)
        {
            
            obj.enabled = false;
        };
        
        Instantiate(_soul, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
    public int GetLife()
    {
        return _life;
    }
}
