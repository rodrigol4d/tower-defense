using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCanvas : MonoBehaviour
{
    [SerializeField]
    private Text _enemyTextCountDown;
    [SerializeField]
    private Destination destination;

    [SerializeField]
    private Text _enemyTextSoulCount;
    [SerializeField]
    private Soul_Collector _soulCollector;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyTextCountDown.text = "Count: " + destination.GetEnemiesLeft();
        _enemyTextSoulCount.text = "Souls: " + _soulCollector.GetSouls();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyTextCountDown.text = "Count: " + destination.GetEnemiesLeft();
        _enemyTextSoulCount.text = "Souls: " + _soulCollector.GetSouls();

    }
}
