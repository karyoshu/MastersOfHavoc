using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicComplete : MonoBehaviour {

    public void OnCinematicComplete()
    {
        SceneManager.LoadScene(2);
    }
}
