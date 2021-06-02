using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Types
{
    Gun,
    Magic,
}

public class Placable : MonoBehaviour
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Types _type;
   
}
