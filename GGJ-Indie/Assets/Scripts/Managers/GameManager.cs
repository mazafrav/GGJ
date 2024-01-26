using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        UiManager.UpdateBackground(progression.Equals(0f) ? 0f : Math.Min(progression / maxProgression, 1));
        progression += Time.deltaTime * 5;
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
        Debug.Log("Progression: " + progression);
    }

    public Player GetPlayer()
    {
        return player;
    }
}
