using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberrySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _strawberryPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _shootingTransform;
    [SerializeField] private Rigidbody _launcherRB;

    private Vector3 m_EulerAngleVelocity;
    private Vector3 m_EulerAngleVelocityBackwards;

    private bool _strawberryLoaded= true;
    private bool _launched;
    [SerializeField] private int _maxAmmo;
    private int _currentAmmo;

    // Start is called before the first frame update
    void Start()
    {
        m_EulerAngleVelocity = new Vector3(360, 0, 0);
        m_EulerAngleVelocityBackwards = new Vector3(-360, 0, 0);
        LoadStrawberry();
        _currentAmmo = _maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadStrawberry()
    {
        GameObject strawberry = Instantiate(_strawberryPrefab, _shootingTransform.position, _shootingTransform.rotation);
        _strawberryLoaded = true;
        _currentAmmo--;
    }

    private void Launch()
    {
        //Debug.Log(_launcherRB.rotation.eulerAngles);

        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        _launcherRB.AddTorque(m_EulerAngleVelocity, ForceMode.Impulse);

        //If Rotation complete
        if (LaunchComplete())
        {
            _strawberryLoaded = false;
            _launched = true;
        }
    }

    private bool LaunchComplete()
    {
        //Debug.Log(_launcherRB.rotation.eulerAngles.x);
        return _launcherRB.rotation.eulerAngles.x >= 334f;
    }

    private bool ResetComplete()
    {
        //Debug.Log(_launcherRB.rotation.eulerAngles.x);
        return _launcherRB.rotation.eulerAngles.x <= 276f;
    }

    private void ResetPosition()
    {
        //Rotate backwards

        //Debug.Log(_launcherRB.rotation.eulerAngles);

        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocityBackwards * Time.fixedDeltaTime);
        _launcherRB.AddTorque(m_EulerAngleVelocityBackwards, ForceMode.Impulse);

        //Position reset to initial launch
        if (ResetComplete())
        {
            LoadStrawberry();
            _launched = false;
        }
    }

    void FixedUpdate()
    {
        if (_strawberryLoaded && !_launched)
        {
            Launch();

        }else
        {
            //Resetting position
            //Debug.Log("Reseting");
            if (NoAmmo())
            {
                Debug.Log("No Ammo");
                Destroy(gameObject);
            }else
            {
                ResetPosition();
            }
        }
        
    }

    private bool NoAmmo()
    {
        return _currentAmmo == 0;
    }
}
