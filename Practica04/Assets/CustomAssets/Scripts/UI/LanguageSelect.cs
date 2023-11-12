using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LanguageSelect : MonoBehaviour
{
    private List<Locale> _locales;
    private int _localeIndex;

    private bool IsInitialized;


    // Start is called before the first frame update
    void Start()
    {
        _locales = LocalizationSettings.AvailableLocales.Locales;
        LocalizationSettings.InitializationOperation.Completed += FinishLoadingLocalization;
    }

    private void FinishLoadingLocalization(AsyncOperationHandle<LocalizationSettings> handle)
    {
        _localeIndex = 0;
        LocalizationSettings.SelectedLocale = _locales[_localeIndex];
    }

    public void NextLocale()
    {
        var index = Math.Abs(++_localeIndex) % _locales.Count;
        Debug.Log(index);
        LocalizationSettings.SelectedLocale = _locales[index];

    }

    public void PreviousLocale()
    {
        var index = Math.Abs(--_localeIndex) % _locales.Count;
        Debug.Log(index);
        LocalizationSettings.SelectedLocale = _locales[index];
    }

}
