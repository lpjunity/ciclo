using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField] private float _rayDistance;
    [SerializeField] private GameObject _buildingsParent;

    private Building _buildingSelected;
    private BoxCollider _buildingCollider;
    private bool _isAbleToCreate;
    private bool _isAbleToPay;
    private bool _hasResourcesToBuild;
    private LayerMask _mask;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _mask = ~(1 << LayerMask.NameToLayer("Ground"));
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
                _isAbleToPay = GameManager.Instance.HasMoneyForBuilding(_buildingSelected.Price);
                _hasResourcesToBuild = _isAbleToCreate && _isAbleToPay;
                if (!_isAbleToCreate)
                {
                    //UIManager.Instance.ShowMessage();
                }
                if (!_isAbleToPay)
                {
                    UIManager.Instance.ShowNotEnoughMoneyWarning();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (_hasResourcesToBuild)
        {
            CreateBuilding();
            _isAbleToCreate = false;
            _hasResourcesToBuild = false;
        }
    }

    void OnDisable()
    {
        _buildingCollider = null;
    }

    public void SelectBuilding(Building building)
    {
        _buildingSelected = building;
        _buildingCollider = _buildingSelected.GetComponent<BoxCollider>();
    }

    private void CreateBuilding()
    {
        Instantiate(_buildingSelected.gameObject, _buildingSelected.transform.position, _buildingSelected.transform.rotation, _buildingsParent.transform);
        GameManager.Instance.SpendMoney(_buildingSelected.Price);
    }
}
