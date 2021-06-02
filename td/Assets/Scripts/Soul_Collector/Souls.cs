using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{
    [SerializeField]
    private int _soulValue;
    [SerializeField]
    private Soul_Collector _soulCollector;

    private float speed = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        _soulCollector = GameObject.Find("Soul_Collector").GetComponent<Soul_Collector>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_soulCollector!= null)
        {
            transform.Translate((_soulCollector.transform.position - transform.position).normalized * 0.02f * speed);

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Soul_Collector")
        {

            _soulCollector.SetSouls(_soulValue);
            Destroy(this.gameObject);

        }
       
    }
}
