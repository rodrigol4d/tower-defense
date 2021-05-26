using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject tower;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        { 

        }
        
    }

    public Vector3 GetMousePosition()
    {
        return cam.ScreenToViewportPoint(Input.mousePosition);
    }
}
