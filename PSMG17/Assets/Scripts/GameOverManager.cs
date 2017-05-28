using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public Transform[] players;
    public Transform[] healthBars;
    public Transform[] enemies;

    private Button resumeButton;
    private Text gameOverText;
    private Transform background;

    private bool gameOver = false;

    void Awake()
    {
        initGameOverScreen();
    }

    private void initGameOverScreen()
    {
        resumeButton = GetComponentInChildren<Button>();
        gameOverText = GetComponentInChildren<Text>();
        background = transform.FindChild("Background");

        resumeButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }

    void Update()
    {
        checkIfPlayersAreDead();
        showGameOverScreen();
    }

    private void showGameOverScreen()
    {
        if (gameOver)
        {
            resumeButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
        }
    }

    private void checkIfPlayersAreDead()
    {
        // sets gameOver variable to true, if both players are deactivated at the same time
        int deadPlayers = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].gameObject.activeSelf)
            {
                deadPlayers++;
            }
        }

        if (deadPlayers == players.Length)
        {
            gameOver = true;
        }
    }

    public void Resume()
    {
        gameOver = false;
        resumeButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);

        reactivatePlayers();
    }

    private void reactivatePlayers()
    {
        foreach (Transform player in players)
        {
            // reactivates players and sets their current health back to max health
            player.gameObject.SetActive(true);
            player.gameObject.GetComponent<HealthbarController>().currentHealth = 
                player.gameObject.GetComponent<HealthbarController>().maxHealth;

            // TODO: set players to last saferoom
            player.SetPositionAndRotation(new Vector3(0, -2, 0), new Quaternion());
        }

        foreach (Transform healthBar in healthBars)
        {   
            // reactivates healthbars
            healthBar.gameObject.SetActive(true);
        }

        foreach(Transform enemy in enemies)
        {
            // reset enemy position and active status
            enemy.gameObject.SetActive(true);
            enemy.SetPositionAndRotation(new Vector3(0, 2, 0), new Quaternion());
        }
    }
}
