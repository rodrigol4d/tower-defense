using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableBluePrint : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject;

    [SerializeField]
    private Material _invalidColor;

    [SerializeField]
    private Material _validColor;

    public bool _hasCollided { get; private set; }


   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawCube(transform.position, transform.localScale);

    }


    public void InstantiateGameObject() {
        Debug.Log("Instantiate acionado");
        Instantiate(_gameObject, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }


    private void OnCollisionStay(Collision collision)
    {

        _hasCollided = true;
        GetComponent<MeshRenderer>().material = _invalidColor;

        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer obj in children)
        {
            obj.GetComponent<MeshRenderer>().material = _invalidColor;

        }

    }


    private void OnCollisionExit(Collision collision)
    {

        _hasCollided = false;

        GetComponent<MeshRenderer>().material = _validColor;
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer obj in children)
        {
            obj.GetComponent<MeshRenderer>().material = _validColor;

        }

    }


}
