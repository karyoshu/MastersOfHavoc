using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerControl player;
    bool paused = false;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
        {
            UIManager.Instance.ToggleBuildMenu(false);
            UIManager.Instance.ToggleUpgradeMenu(false);
            Time.timeScale = 0;
            UIManager.Instance.TogglePauseMenu(true);
        }
        else
        {
            Time.timeScale = 1;
            UIManager.Instance.TogglePauseMenu(false);
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }


}
