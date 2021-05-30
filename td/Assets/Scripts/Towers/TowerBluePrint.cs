using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBluePrint : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject;

    [SerializeField]
    private Material _invalidColor;

    [SerializeField]
    private Material _validColor;

    public bool _hasCollided { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateGameObject() {
        Instantiate(_gameObject, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

   
}
