using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColision : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject;

    public bool _hasCollided { get; private set; }
    

    [SerializeField]
    private Material _invalidColor;

    [SerializeField]
    private Material _validColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("um objeto entrou");
        _gameObject = collision.gameObject;
        _hasCollided = true;
        GetComponent<MeshRenderer>().material = _invalidColor;

        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer obj in children)
        {
            obj.GetComponent<MeshRenderer>().material = _invalidColor;

        }



    }
 

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("um objeto saiu");
        _gameObject = null;
        _hasCollided = false;

        GetComponent<MeshRenderer>().material = _validColor;
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer obj in children)
        {
            obj.GetComponent<MeshRenderer>().material = _validColor; 

        }
    }
}
