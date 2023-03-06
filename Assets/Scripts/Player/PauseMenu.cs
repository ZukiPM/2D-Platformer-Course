using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuGO;

    bool paused = false;

    private void Start()
    {
        pauseMenuGO.SetActive(false);
        paused = false;
        GameManager.singleton.isPaused = false;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            GameManager.singleton.isPaused = true;

            pauseMenuGO.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
            GameManager.singleton.isPaused = false;

            pauseMenuGO.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        paused = false;
        GameManager.singleton.isPaused = false;

        pauseMenuGO.SetActive(false);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
