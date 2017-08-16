using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasControls : MonoBehaviour {

    public GameObject HelpCanvas;
    public GameObject Credits;

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ToggleHelp(bool value)
    {
        HelpCanvas.SetActive(value);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToggleCredits(bool value)
    {
        Credits.SetActive(value);
    }
}
