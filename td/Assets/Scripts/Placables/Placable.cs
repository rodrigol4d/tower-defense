using UnityEngine;

public enum Types
{
    machine,
    Magic,
}
public enum PlacableLevel
{
    level_1,
    level_2,
    level_3,

}

public enum Status
{
    idle,
    enemyDetected,
    engaged
}

public class Placable : MonoBehaviour
{
    [Header("Basic Atributtes")]
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected PlacableLevel _level;
    [SerializeField]
    protected GameObject[] _abilitys;
    [SerializeField]
    protected GameObject[] _specialAbilitys;
    [SerializeField]
    protected Types _type;
    [SerializeField]
    protected float _placebleRadius = 1f;
    [SerializeField]
    protected float _lookSpeed = 0.10f;
    [SerializeField]
    protected Animator _animator;
    [SerializeField]
    protected Status _status;






}
