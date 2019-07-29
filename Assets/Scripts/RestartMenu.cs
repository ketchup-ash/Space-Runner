using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour {

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit() {
        Application.Quit();
    }

}

