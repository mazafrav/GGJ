using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox;
    private string text0 = "Face your demons in your sleep.\n";
    private string text1 = "You did great for tonight, but the path does not end here\nYou will have some help in your next dream.\n\nBuff: Dream companion";
    private string text2 = "You did great for tonight, but the path does not end here\nYou will have more mental clarity in your next dream.\n\nBuff: Speed increased";
    private string text3 = "You did great for tonight, but the path does not end here\nYou will be unstoppable on your next dream.\n\nBuff: More damage";
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
        SceneManager.LoadScene(1);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
    }

}
