using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ElementsCountController _counterController;

    private int _tree01Counter;
    private int _tree02Counter;
    private int _houseCounter;
    private int _wellCounter;

    public bool CameraMode { get; private set; }
    public bool BuildingMode { get; private set; }



    private void OnEnable()
    {
        _counterController = GameObject.FindGameObjectWithTag("CounterController")?.GetComponent<ElementsCountController>();
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void DeactivateModes()
    {
        CameraMode = false;
        BuildingMode = false;
    }

    public void ActivateCameraMode()
    {
        CameraMode = true;
        BuildingMode = false;
    }

    public void ActivateBuildingMode()
    {
        BuildingMode = true;
        CameraMode = false;
    }

    public void AddTree()
    {
        _tree01Counter++;
        if (_counterController)
        {
            _counterController.UpdateTree01Counter(_tree01Counter.ToString());
        }
    }

    public void AddHouse()
    {
        _houseCounter++;
        if (_counterController)
        {
            _counterController.UpdateHouse01Counter(_houseCounter.ToString());
        }
    }
}
