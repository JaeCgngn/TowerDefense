using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1f)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0f)
        {
            ResumeGame();
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }   

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }


}
