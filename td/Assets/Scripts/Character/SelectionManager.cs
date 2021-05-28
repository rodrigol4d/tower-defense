using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Camera fpsCam;
    [SerializeField]
    public GameObject placableObjectPrefab;
    [SerializeField]
    private KeyCode newObjectHotKey = KeyCode.F1;
    [SerializeField]
    private GameObject currentPlacableObject;
   
    void Update()
    {
        if(currentPlacableObject != null)
        {
             Shoot();

        }
        
        HandleNewObjectHotKey();
       


    }
    void HandleNewObjectHotKey()
    {
        if (Input.GetKeyDown(newObjectHotKey))
        {
            if(currentPlacableObject == null)
            {
                currentPlacableObject = Instantiate(placableObjectPrefab);
            }

        }
    }


    void Shoot()
    {
        RaycastHit hit;
       if( Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hit, 100f)){
       
                Debug.Log(hit.point);
                currentPlacableObject.transform.position = hit.point;
               // Posicionamento com curvatura -->  currentPlacableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
}
