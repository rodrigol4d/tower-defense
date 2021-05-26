using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemys;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyInstantiationCoRoutine());
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }
    private IEnumerator EnemyInstantiationCoRoutine()
    {
     
         
        while (true)
        {
            if (enemys != null)
            {
                Instantiate(enemys[Random.Range(0, enemys.Length)], transform.position, transform.rotation);
            }
            
            yield return new WaitForSeconds(3f);
        }
    }
}
