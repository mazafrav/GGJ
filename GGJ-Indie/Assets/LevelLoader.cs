using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    // Update is called once per frame
    public void StartLoadingLevel(int levelnumber)
    {
        StartCoroutine(LoadLevel(levelnumber));
    }

    IEnumerator LoadLevel(int levelnumber)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelnumber);
    }
}
