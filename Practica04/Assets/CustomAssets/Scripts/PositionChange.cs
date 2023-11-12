using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChange : MonoBehaviour
{
    [SerializeField] private Transform _firstPersonTransformPoint;
    [SerializeField] private Transform _thirdPersonTransformPoint;

    //We could play with cinemachine and priorities but...

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.C)) {
            ChangeCameraMode();
        }*/
    }

    public void ChangeCameraMode()
    {
        if(_thirdPersonTransformPoint &&  _firstPersonTransformPoint) {
            if (_firstPersonTransformPoint.position.Equals(transform.position))
            {
                transform.SetPositionAndRotation(_thirdPersonTransformPoint.position, _thirdPersonTransformPoint.rotation);
            }else
            {
                transform.SetPositionAndRotation(_firstPersonTransformPoint.position, _firstPersonTransformPoint.rotation);
            }
        }
    }

}
