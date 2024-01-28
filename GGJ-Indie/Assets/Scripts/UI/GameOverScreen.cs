using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox;
    private string text0 = "Face your demons in your sleep.\n";
    private string text1 = "You will have some help in your next dream.\nBuff: DVD player I guess";
    private string text2 = "You will have more mental clarity in your next dream.\nBuff: Speed increased";
    private string text3 = "You will be unstoppable on your next dream.\nBuff: More damage";
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        int buff = 0;
        textBox.SetText(text0);
        if (gameManager.GetComponent<GameManager>() != null)
        {
            buff = gameManager.GetComponent<GameManager>().getActualBuff();

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
        SceneManager.LoadScene(0);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(1);
    }

}
