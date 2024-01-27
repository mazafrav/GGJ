using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewDream()
    {
        SceneManager.LoadScene(0);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(1);
    }

}
