using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Camera fpsCam;
    [SerializeField]
    public GameObject[] placableObjectsPrefab = new GameObject[10];
    [SerializeField]
    private KeyCode[] _objectsHotKey = new KeyCode[11];
    [SerializeField]
    private GameObject currentPlacableObject;
    [SerializeField]
    private LayerMask layer;

     void Start()
    {
        
    }

  

    void SelectObject()
    {
        if (Input.GetKeyDown(_objectsHotKey[0])){
            
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[0])
            {
                Debug.Log("Key pressed" + _objectsHotKey[0]);
                currentPlacableObject = Instantiate(placableObjectsPrefab[0]);

            }
            
        }
        if (Input.GetKeyDown(_objectsHotKey[1]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[1])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[1]);

            }
        }
        if (Input.GetKeyDown(_objectsHotKey[2]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[2])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[2]);

            }
        }
        if (Input.GetKeyDown(_objectsHotKey[3]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[3])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[3]);

            }
        }
        if (Input.GetKeyDown(_objectsHotKey[4]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[4])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[4]);

            }
        }
        if (Input.GetKeyDown(_objectsHotKey[5]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[5])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[5]);

            }
        }
        if (Input.GetKeyDown(_objectsHotKey[6]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[6])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[6]);

            }
        }
        if (Input.GetKeyDown(_objectsHotKey[7]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[7])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[7]);

            }
        }
        if (Input.GetKeyDown(_objectsHotKey[8]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[8])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[8]);

            }
        }
        if (Input.GetKeyDown(_objectsHotKey[9]))
        {
            Destroy(this.currentPlacableObject);
            if (placableObjectsPrefab[9])
            {
                currentPlacableObject = Instantiate(placableObjectsPrefab[9]);

            }
        }

        if (Input.GetKeyDown(_objectsHotKey[10]))
        {
            
           
                Destroy(this.currentPlacableObject);

            
        }





    }

    void Update()
    {
        if(currentPlacableObject != null)
        {
            ObjectPlacement();

        }
        SelectObject();
        
      



    }


    void ObjectPlacement()
    {
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;
        PlacableBluePrint placableBluePrint = currentPlacableObject.GetComponent<PlacableBluePrint>();

     



        if ( Physics.Raycast(ray,out hit,Mathf.Infinity)){

            // Se ligar o collider deste objeto, ira dar problema na lógica atual

            if (hit.transform.gameObject.layer == 6 )
            {
                Debug.Log(hit.transform.gameObject.layer);

                currentPlacableObject.transform.position = hit.point + new Vector3(0.0f, 0.5f, 0);

                if (Input.GetMouseButtonDown(0) && !placableBluePrint._hasCollided)
                {

                    Debug.Log("Mouse Clicked");
                    placableBluePrint.InstantiateGameObject();

                    currentPlacableObject = null;

                }
                



          }
            else { }
         

            Debug.DrawLine(ray.origin,hit.point,Color.green);
           
          //    currentPlacableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.blue);
        }
    }


}
