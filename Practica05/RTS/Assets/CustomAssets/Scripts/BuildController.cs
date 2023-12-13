using UnityEngine;
using UnityEngine.EventSystems;

public class BuildController : MonoBehaviour
{
    [SerializeField] private float _rayDistance;
    [SerializeField] private GameObject _buildingsParent;

    private GameObject _buildingSelected;
    private GameObject _buildingPreview;
    private BoxCollider _buildingCollider;
    private bool _isAbleToCreate;
    private bool _wantToBuild;
    private LayerMask _mask;

    [SerializeField] private GameObject _warningPanel;

    private EventSystem _eventSystem;

    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        _mask = ~(1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
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
            bool buildingInCollision = Physics.CheckBox(hit.point, _buildingCollider.size/2, _buildingCollider.transform.rotation, _mask);
            
            //test.transform.position = hit.point-Camera.main.transform.position;
            //test.transform.rotation = _buildingCollider.transform.rotation;
            //test.transform.localScale = _buildingCollider.size;

            if (!buildingInCollision && hit.collider.gameObject.CompareTag("Buildeable"))
            {
                _buildingPreview.transform.position = hit.point;
                //Debug.Log($"Position: {hit.point}");
                //Debug.Log(hit.collider.name);
                _isAbleToCreate = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _wantToBuild = _isAbleToCreate && !_eventSystem.IsPointerOverGameObject();
                if (!_isAbleToCreate)
                {
                    ShowBuildingUnavailableMessage();
                    Invoke("ShowBuildingUnavailableMessage", 2f);
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if(_wantToBuild)
        {
            CreateBuilding();
            _isAbleToCreate = false;
            _wantToBuild = false;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.ActivateBuildingMode();
        _eventSystem = EventSystem.current;
    }

    private void OnDisable()
    {
        _buildingSelected = null;
        GameManager.Instance.DeactivateModes();   
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
        _buildingPreview = Instantiate(prefab, _buildingsParent.transform);
        _buildingCollider = _buildingPreview.GetComponent<BoxCollider>();
    }

    private void CreateBuilding()
    {
        switch (_buildingPreview.tag)
        {
            case "Tree":
                GameManager.Instance.AddTree();
                break;
            case "Tree2":
                GameManager.Instance.AddTree2();
                break;
            case "House":
                GameManager.Instance.AddHouse();
                break;
            case "Well":
                GameManager.Instance.AddWell();
                break;
        }

        _buildingPreview = Instantiate(_buildingSelected, _buildingPreview.transform.position, _buildingPreview.transform.rotation, _buildingsParent.transform);
        
    }

    private void ShowBuildingUnavailableMessage()
    {
        _warningPanel.SetActive(!_warningPanel.activeSelf);
    }
}
