using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private LevelLoader levelLoader;
    [SerializeField] private TMP_Text textBox;
    private string text0 = "Face your demons in your sleep.\n";
    private string text1 = "Advantage for next dream:\n\nDVD COMPANION \nThe DVD will damage your enemies";
    private string text2 = "Advantage for next dream:\n\nSPEED INCREASE\nBuff: Speed increased";
    private string text3 = "Advantage for next dream:\n\nDAMAGE INCREASE\nBuff: More damage";
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        int buff = 0;
        textBox.SetText(text0);
        if (gameManager.GetComponent<GameManager>() != null)
        {
            buff = gameManager.GetComponent<GameManager>().getCurrentBuff();

            if (buff == 1)
            {
                textBox.text = text1;
            }
            if (buff == 2)
            {
                textBox.text = text2;
            }
            if (buff == 3)
            {
                textBox.text = text3;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewDream()
    {
        levelLoader.StartLoadingLevel(1);
    }

    public void TitleScreen()
    {
        levelLoader.StartLoadingLevel(0);
    }

}
