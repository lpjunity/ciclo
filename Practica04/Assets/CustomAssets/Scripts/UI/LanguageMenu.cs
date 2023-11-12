using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LanguageMenu : MonoBehaviour
{
    private StringTable _uiTextsTable;
    [SerializeField] private GameObject _languageListPanel;
    [SerializeField] private GameObject _languageButtonPrefab;

    private List<Button> _languagesButtonsInstantiated;
    private List<UnityEngine.Localization.Locale> _locales;
    private bool isInitialized;

    // Start is called before the first frame update
    void Start()
    {
        _languagesButtonsInstantiated = new List<Button>();

        _uiTextsTable = LocalizationSettings.StringDatabase?.GetTable("UITexts");

        _locales = LocalizationSettings.AvailableLocales.Locales;
    }

    public void FillLanguageScreen()
    {
        if (_languageListPanel && !isInitialized)
        {
            foreach (var locale in _locales)
            {
                GameObject _createdObject = Instantiate(_languageButtonPrefab, _languageListPanel.transform);
                Button _createdButton = _createdObject.GetComponent<Button>();
                _createdButton.name = locale.Identifier.Code;
                TextMeshProUGUI _createdButtonText = _createdButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                StringTableEntry buttonTextWithLocale = _uiTextsTable.GetEntry(locale.Identifier.Code);

                LocalizeStringEvent localizedObj = _createdButtonText.AddComponent<LocalizeStringEvent>();


                localizedObj.SetTable(_uiTextsTable.TableCollectionName);
                localizedObj.SetEntry(buttonTextWithLocale.Key);

                localizedObj.OnUpdateString.AddListener((string textChanged) => { _createdButtonText.text = textChanged; });

                _createdButton.onClick.AddListener(() => {
                    LocalizationSettings.SelectedLocale = locale;

                });

                if (buttonTextWithLocale != null)
                {
                    Debug.Log(buttonTextWithLocale.Value);
                    _createdButtonText.text = buttonTextWithLocale.Value;
                }

                _languagesButtonsInstantiated.Add(_createdButton);
            }
            isInitialized = true;
        }
    }

    public void MarkSelected()
    {
        foreach (var button in _languagesButtonsInstantiated)
        {
            if (LocalizationSettings.SelectedLocale.Identifier == button.gameObject.name)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
        }
    }

}
