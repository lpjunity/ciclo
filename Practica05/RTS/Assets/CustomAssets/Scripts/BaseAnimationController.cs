using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BaseAnimationController : MonoBehaviour
{
    public static BaseAnimationController Instance { get; private set; }

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

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    public void DisablePanelScalingY(GameObject panel)
    {
        LeanTween.scaleY(panel, 0f, 1f).setOnComplete(() => { panel.SetActive(false);});
    }

    public void EnablePanelScalingY(GameObject panel)
    {
        panel.SetActive(true);
        LeanTween.scaleY(panel, 1f, 1f).setDelay(1f);
    }

    public void DisablePanelScalingX(GameObject panel)
    {
        LeanTween.scaleX(panel, 0f, 1f).setOnComplete(() => { panel.SetActive(false); });
    }

    public void EnablePanelScalingX(GameObject panel)
    {
        panel.SetActive(true);
        LeanTween.scaleX(panel, 1f, 1f).setDelay(1f);
    }
}
