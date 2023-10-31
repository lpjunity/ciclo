using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int _sceneIndex;

    public void ChangeWithDelay(int sceneIndex)
    {
        _sceneIndex = sceneIndex;
        Invoke("ChangeScene", 2f);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
