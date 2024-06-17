using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class LanguageSelect : MonoBehaviour
{
    [SerializeField] private LocalizationSettings _settings;
    private StringTable _uiTextsTable;
    [SerializeField] private GameObject _languageListPanel;
    [SerializeField] private GameObject _languageButtonPrefab;
    private List<Locale> _locales;
    private Locale _currentLocale;

    private List<GameObject> _languagesListInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        _languagesListInstantiated = new List<GameObject>();
        _locales = _settings.GetAvailableLocales().Locales;

        _uiTextsTable = _settings.GetStringDatabase()?.GetTable("UITexts");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillLanguageScreen()
    {
        if (_languageListPanel)
        {
            foreach (var locale in _locales)
            {
                GameObject _createdObject = Instantiate(_languageButtonPrefab, _languageListPanel.transform);
                Button _createdButton = _createdObject.GetComponent<Button>();
                TextMeshProUGUI _createdButtonText = _createdButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                StringTableEntry buttonTextWithLocale = _uiTextsTable.GetEntry(locale.Identifier.Code);
                Debug.Log($"Button: {buttonTextWithLocale}");
                _createdButton.onClick.AddListener(() => { 
                    _settings.SetSelectedLocale(locale);
                    //Change text of the instantiated list
                    foreach(var language in _languagesListInstantiated)
                    {

                    }
                });

                if (buttonTextWithLocale != null)
                {
                    _createdButtonText.text = buttonTextWithLocale.LocalizedValue;
                }

                _languagesListInstantiated.Add(_createdObject);
            }
        }
    }

    public void RemoveLanguageScreen()
    {
        foreach (var button in _languagesListInstantiated)
        {
            Destroy(button);
        }
    }

    /*public Locale NextLocale()
    {

    }*/

    /*public Localte PreviousLocale()
    {

    }*/
}
