using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    public PauseUI pauseUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1f)
        {
            pauseUI.OpenPause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0f)
        {
            pauseUI.ClosePause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }


    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("TowerDefense");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("TowerDefense");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    // public void PauseGame()
    // {
    //     Time.timeScale = 0f;
    // }   

    // public void ResumeGame()
    // {
    //     Time.timeScale = 1f;
    // }


}
