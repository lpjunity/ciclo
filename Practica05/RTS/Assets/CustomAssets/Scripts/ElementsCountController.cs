using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementsCountController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tree01;
    [SerializeField] private TextMeshProUGUI _tree02;
    [SerializeField] private TextMeshProUGUI _house01;
    [SerializeField] private TextMeshProUGUI _well01;

    private void Start()
    {
        _tree01.text = string.Empty;
        _tree02.text = string.Empty;
        _house01.text = string.Empty;
        _well01.text = string.Empty;
    }

    public void UpdateTree01Counter(string text)
    {
        _tree01.text = text;
    }

    public void UpdateTree02Counter(string text)
    {
        _tree02.text = text;
    }

    public void UpdateHouse01Counter(string text)
    {
        _house01.text = text;
    }

    public void UpdateWell01Counter(string text)
    {
        _well01.text = text;
    }

}
