using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceOnDemand : MonoBehaviour
{
    private Animator _animatorController;
    private static int _dance = Animator.StringToHash("dance");

    private void Awake()
    {
        _animatorController = GetComponent<Animator>();
    }

    public void StartStopDancing()
    {
        _animatorController.SetBool(_dance, !_animatorController.GetBool(_dance));
    }
}
