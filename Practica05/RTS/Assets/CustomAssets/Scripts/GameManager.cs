using System;
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
    public void AddTree2()
    {
        _tree02Counter++;
        if (_counterController)
        {
            _counterController.UpdateTree02Counter(_tree02Counter.ToString());
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

    public void AddWell()
    {
        _wellCounter++;
        if (_counterController)
        {
            _counterController.UpdateWell01Counter(_wellCounter.ToString());
        }
    }

    public void RemoveTree()
    {
        _tree01Counter--;
        if (_counterController)
        {
            _counterController.UpdateTree01Counter(_tree01Counter.ToString());
        }
    }

    public void RemoveTree2()
    {
        _tree02Counter--;
        if (_counterController)
        {
            _counterController.UpdateTree02Counter(_tree02Counter.ToString());
        }
    }

    public void RemoveHouse()
    {
        _houseCounter--;
        if (_counterController)
        {
            _counterController.UpdateHouse01Counter(_houseCounter.ToString());
        }
    }

    public void RemoveWell()
    {
        _wellCounter--;
        if (_counterController)
        {
            _counterController.UpdateWell01Counter(_wellCounter.ToString());
        }
    }

    public void RemoveAllTrees()
    {
        _tree01Counter = 0;
        if (_counterController)
        {
            _counterController.UpdateTree01Counter(_tree01Counter.ToString());
        }
    }

    public void RemoveAllTrees2()
    {
        _tree02Counter = 0;
        if (_counterController)
        {
            _counterController.UpdateTree02Counter(_tree02Counter.ToString());
        }
    }

    public void RemoveAllHouses()
    {
        _houseCounter = 0;
        if (_counterController)
        {
            _counterController.UpdateHouse01Counter(_houseCounter.ToString());
        }
    }

    public void RemoveAllWells()
    {
        _wellCounter = 0 ;
        if (_counterController)
        {
            _counterController.UpdateWell01Counter(_wellCounter.ToString());
        }
    }
}
