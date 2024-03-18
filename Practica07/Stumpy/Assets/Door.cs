using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private static int _wantToOpen = Animator.StringToHash("wantToOpen");
    private bool _playerIsClose;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player is close");
            _playerIsClose = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsClose = false;

        }
    }

    public void OpenDoor()
    {
        if(_playerIsClose)
        {
            _animator.SetBool(_wantToOpen, true);
        }
    }
}
