using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    
    public void OnPausePress() {
        Time.timeScale = 0f;
    }

    public void OnResumePress() {
        Time.timeScale = 1f;
    }

    public void Quit() {
        Application.Quit();
    }

}
