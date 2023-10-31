using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _zOffset;

    private const float TIME = 2f;


    public void UIMoveLeft(GameObject uiObject)
    {
        Vector3 position = uiObject.transform.position;
        LeanTween.move(uiObject, new Vector3(position.x + _xOffset, position.y + _yOffset, position.z + _zOffset), TIME).setEaseInElastic();
    }
}
