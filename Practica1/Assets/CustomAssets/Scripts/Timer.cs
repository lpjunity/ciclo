using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _seconds;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private GameObject _defeatPanel;
    [SerializeField] private TextMeshProUGUI _defeat;

    private bool _defeated;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _seconds -= Time.deltaTime;
        _timerText.text = _seconds.ToString();
        if( _seconds < 0 && !_defeated)
        {
            Instantiate(_defeat, _defeatPanel.transform);
            Time.timeScale = 0;
            _defeatPanel.SetActive(true);
            _defeated = true;
        }
    }

}
