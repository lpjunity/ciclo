using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveController : MonoBehaviour
{
    [SerializeField] private float _rayDistance;
    private LayerMask _layerMask;
    private bool _pointingMode;
    private bool _wantToRemove;
    private GameObject _removeGameobject;

    private void OnEnable()
    {
        _pointingMode = true;
        _wantToRemove = false;
        _layerMask = ~(1 << LayerMask.NameToLayer("Ground"));
        _removeGameobject = null;
    }

    public void CleanElementToRemove()
    {
        _pointingMode = true;
    }

    private void Update()
    {
        if (_pointingMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _rayDistance, _layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);
                _removeGameobject = hit.collider.gameObject;
                if (Input.GetMouseButtonDown(0))
                {
                    _wantToRemove = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_wantToRemove)
        {
            RemoveBuilding();
            _wantToRemove = false;
        }
    }

    private void RemoveBuilding()
    {
        if (_removeGameobject != null)
        {
            Destroy(_removeGameobject);
            RemoveCounter(_removeGameobject.tag);
        }
    }

    private void RemoveCounter(string tag)
    {
        switch (tag)
        {
            case "Tree":
                GameManager.Instance.RemoveTree();
                break;
            case "Tree2":
                GameManager.Instance.RemoveTree2();
                break;
            case "House":
                GameManager.Instance.RemoveHouse();
                break;
            case "Well":
                GameManager.Instance.RemoveWell();
                break;
        }
    }

    private void CleanCounter(string tag)
    {
        switch (tag)
        {
            case "Tree":
                GameManager.Instance.RemoveAllTrees();
                break;
            case "Tree2":
                GameManager.Instance.RemoveAllTrees2();
                break;
            case "House":
                GameManager.Instance.RemoveAllHouses();
                break;
            case "Well":
                GameManager.Instance.RemoveAllWells();
                break;
        }
    }

    public void RemoveElementWithTag(string tagToRemove)
    {
        _pointingMode = false;
        GameObject[] elementsToRemove = GameObject.FindGameObjectsWithTag(tagToRemove);
        foreach (GameObject elementToRemove in elementsToRemove)
        {
            Destroy(elementToRemove);
        }
        CleanCounter(tagToRemove);
    }

}
