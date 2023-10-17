using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _currentViewPoint;
    [SerializeField] private Transform _firstPersonViewpoint;
    [SerializeField] private Transform _thirdPersonViewpoint;
    private float _speedChange;

    // Start is called before the first frame update
    void Start()
    {
        _currentViewPoint = _thirdPersonViewpoint.transform;
        _speedChange = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentViewPoint.Equals(_thirdPersonViewpoint))
        {
            Camera.main.transform.LookAt(_playerTransform);
        }
        
        transform.position = Vector3.Slerp(transform.position, _currentViewPoint.position, _speedChange * Time.deltaTime);

    }

    public void ChangeTransform()
    {
        if (_currentViewPoint.Equals(_firstPersonViewpoint))
        {
            _currentViewPoint = _thirdPersonViewpoint.transform;
            _speedChange = 2f;
        }
        else
        {
            _currentViewPoint = _firstPersonViewpoint;
            _speedChange = 10f;
        }
    }
}
