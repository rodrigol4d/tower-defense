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
    private int _level;
    [SerializeField]
    private GameObject[] _abilitys;
    [SerializeField]
    private GameObject[] _specialAbilitys;
    [SerializeField]
    private Types _type;
   
}
