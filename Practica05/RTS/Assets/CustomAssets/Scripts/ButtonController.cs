using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    
    public void SelectOptionFromMainMenu(GameObject panelToEnable)
    {
        BaseAnimationController.Instance.DisablePanelScalingY(gameObject.transform.parent.gameObject);
        BaseAnimationController.Instance.EnablePanelScalingX(panelToEnable);
    }

    public void GoBackToMainMenu(GameObject panelToEnable)
    {
        BaseAnimationController.Instance.DisablePanelScalingX(gameObject.transform.parent.gameObject);
        BaseAnimationController.Instance.EnablePanelScalingY(panelToEnable);
    }

    public void SelectBuilding(GameObject prefab)
    {
        
    }

}
