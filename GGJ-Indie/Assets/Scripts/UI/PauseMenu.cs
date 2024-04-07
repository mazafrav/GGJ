using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    [SerializeField]
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.visible=false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;  
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        levelLoader.StartLoadingLevel(0);
    }
}
