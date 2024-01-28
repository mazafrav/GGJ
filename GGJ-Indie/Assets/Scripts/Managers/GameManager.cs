using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private float progression = 0f;

    [SerializeField]
    private float maxProgression = 100f;

    [SerializeField]
    private float progressionPerKill = 1f;

    public static int currentBuff = 0;

    public static GameManager Instance { get; private set; }

    public static UIManager UiManager { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UiManager = GetComponentInChildren<UIManager>();

        if (!player) return;
        player.onEnemyKill += OnEnemyKill;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            UiManager.UpdateBackground(progression.Equals(0f) ? 0f : Math.Min(progression / maxProgression, 1));
        }
        //progression += Time.deltaTime * 5;
        // Debug.Log(progression);
    }

    private void OnDestroy()
    {
        if (!player) return;
        player.onEnemyKill -= OnEnemyKill;
    }

    private void OnEnemyKill(Enemy enemy)
    {      
        progression = Math.Max(progression + progressionPerKill, maxProgression);
    }

    public float GetMaxProgression()
    {
        return maxProgression;
    }

    public void IncreaseProgression()
    {
        progression += 1.0f;
    }

    public float GetProgression()
    {
        return progression;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void setCurrentBuff(int buff)
    {
        currentBuff = buff;
    }

    public int getCurrentBuff()
    {
        return currentBuff;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(3);
    }


    public void Win()
    {
       
        SceneManager.LoadScene(2);
        
    }
}
