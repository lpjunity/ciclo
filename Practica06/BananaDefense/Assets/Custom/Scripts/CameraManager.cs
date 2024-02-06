using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _rayDistance;
    [SerializeField] private GameObject _buildingsParent;

    private GameObject _buildingSelected;
    private GameObject _buildingPreview;
    private BoxCollider _buildingCollider;
    private bool _isAbleToCreate;
    private bool _wantToBuild;
    private LayerMask _mask;

    [SerializeField] GameObject _test;

    // Start is called before the first frame update
    void Start()
    {
        _mask = ~(1 << LayerMask.NameToLayer("Ground"));
        SelectBuilding(_test);
    }

    void Update()
    {
        if (_buildingSelected == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);
            bool buildingInCollision = Physics.CheckBox(hit.point, _buildingCollider.size / 2, _buildingCollider.transform.rotation, _mask);

            //test.transform.position = hit.point-Camera.main.transform.position;
            //test.transform.rotation = _buildingCollider.transform.rotation;
            //test.transform.localScale = _buildingCollider.size;

            if (!buildingInCollision && hit.collider.gameObject.CompareTag("Buildeable"))
            {
                _buildingSelected.transform.position = hit.point;
                //Debug.Log($"Position: {hit.point}");
                //Debug.Log(hit.collider.name);
                _isAbleToCreate = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _wantToBuild = _isAbleToCreate /*&& !_eventSystem.IsPointerOverGameObject()*/;
                /*if (!_isAbleToCreate)
                {
                    Invoke("ShowBuildingUnavailableMessage", 2f);
                }*/
            }
        }

    }

    private void FixedUpdate()
    {
        if (_wantToBuild)
        {
            CreateBuilding();
            _isAbleToCreate = false;
            _wantToBuild = false;
        }
    }

    /*private void OnEnable()
    {
        GameManager.Instance.ActivateBuildingMode();
        _eventSystem = EventSystem.current;
    }*/

    private void OnDisable()
    {
        /*_buildingSelected = null;
        GameManager.Instance.DeactivateModes();*/
        Destroy(_buildingPreview);
        _buildingCollider = null;
    }

    public void SelectBuilding(GameObject prefab)
    {
        if (_buildingPreview)
        {
            Destroy(_buildingPreview);
        }
        _buildingSelected = prefab;
        //_buildingPreview = Instantiate(prefab, _buildingsParent.transform);
        _buildingCollider = _buildingSelected.GetComponent<BoxCollider>();
    }

    private void CreateBuilding()
    {
        GameObject _buildingSpawn = Instantiate(_buildingSelected, _buildingSelected.transform.position, _buildingSelected.transform.rotation, _buildingsParent.transform);
    }
}
