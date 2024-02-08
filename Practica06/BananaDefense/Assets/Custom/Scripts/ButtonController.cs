using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public void AssignBuilding(Building building)
    {
        CameraManager.Instance.SelectBuilding(building);
    }

    public void ChangeColorToRed(Image image)
    {
        image.color = Color.red;
    }

    public void ChangeColorToWhite(Image image)
    {
        image.color = Color.white;
    }

    public void Play()
    {
        GameManager.Instance.StartGame();
    }

    public void Exit()
    {
        GameManager.Instance.ExitGame();
    }

}
