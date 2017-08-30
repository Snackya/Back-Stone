using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    [SerializeField]
    private Transform[] players;
    [SerializeField]
    private Transform[] enemies;

    private Vector3[] playerRespawnPositions = new Vector3[2] { new Vector3(-178, 68, 0), new Vector3(-178, 61, 0)};

    private Button resumeButton;
    private Button quitButton;
    private Text gameOverText;
    private Transform background;
    private Transform skeleton;

    private bool resumeSelected = false;
    private bool quitSelected = false;
    private bool gameOver = false;

    // NEU
    [SerializeField] private Saferoom room01;
    [SerializeField] private Room02 room02;
    [SerializeField] private Room04 room04;
    [SerializeField] private Saferoom room05;
    [SerializeField] private Room06 room06;
    [SerializeField] private Room08 room08;
    [SerializeField] private Room09 room09;
    [SerializeField] private Saferoom room11;
    [SerializeField] private Room12 room12;
    [SerializeField] private Room13 room13;
    [SerializeField] private Room16 room16;
    [SerializeField] private Room17 room17;
    [SerializeField] private Saferoom room19;
    [SerializeField] private Room20 room20;

    void Awake()
    {
        initGameOverScreen();
    }

    // use this function, whenever players reach a new saferoom
    public void setNewRespawnPosition(Vector3 player1Pos, Vector3 player2Pos)
    {
        playerRespawnPositions[0] = player1Pos;
        playerRespawnPositions[1] = player2Pos;
    }

    private void initGameOverScreen()
    {
        resumeButton = transform.FindChild("ResumeButton").GetComponent<Button>();
        quitButton = transform.FindChild("QuitButton").GetComponent<Button>();
        gameOverText = GetComponentInChildren<Text>();
        background = transform.FindChild("Background");
        skeleton = transform.FindChild("Skeleton");

        resumeButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        skeleton.gameObject.SetActive(false);
    }

    void Update()
    {
        checkIfPlayersAreDead();
        showGameOverScreen();
        NavigateWithKeys();
    }

    private void showGameOverScreen()
    {
        if (gameOver)
        {
            resumeButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
            skeleton.gameObject.SetActive(true);
        }
    }

    private void NavigateWithKeys()
    {
        Navigate();
        HighlightButtons();
        CheckForButtonSelect();
    }

    private void Navigate()
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        if (horizontalInput == 1)
        {
            if (!quitSelected && !resumeSelected)
            {
                resumeSelected = true;
            }
            if (quitSelected && !resumeSelected)
            {
                quitSelected = false;
                resumeSelected = true;
            }
        }
        if(horizontalInput == -1)
        {
            if (!quitSelected && !resumeSelected)
            {
                quitSelected = true;
            }
            if(!quitSelected && resumeSelected)
            {
                resumeSelected = false;
                quitSelected = true;
            }
        }
    }

    private void HighlightButtons()
    {
        if (quitSelected)
        {
            quitButton.GetComponent<Image>().color = quitButton.GetComponent<Button>().colors.highlightedColor;
        }
        else
        {
            quitButton.GetComponent<Image>().color = quitButton.GetComponent<Button>().colors.normalColor;

        }

        if (resumeSelected)
        {
            resumeButton.GetComponent<Image>().color = resumeButton.GetComponent<Button>().colors.highlightedColor;
        }
        else
        {
            resumeButton.GetComponent<Image>().color = resumeButton.GetComponent<Button>().colors.normalColor;
        }
    }

    private void CheckForButtonSelect()
    {
        if (Input.GetButtonDown("Action1"))
        {
            if(resumeSelected && !quitSelected)
            {
                Resume();
            }
            else if(!resumeSelected && quitSelected)
            {
                QuitGame();
            }
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
        quitButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        skeleton.gameObject.SetActive(false);

        reactivatePlayers();
        // NEU
        resetRooms();

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void resetRooms()
    {
        room01.resetRoom();
        room02.resetRoom();
        room04.ResetRoom();
        room05.resetRoom();
        room06.resetRoom();
        room08.ResetRoom();
        room09.ResetRoom();
        room11.resetRoom();
        room12.resetRoom();
        room13.ResetRoom();
        room16.resetRoom();
        room17.ResetRoom();
        room19.resetRoom();
        room20.ResetRoom();
    }

    private void reactivatePlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            // reactivates players and sets their current health back to max health
            players[i].gameObject.SetActive(true);
            players[i].gameObject.GetComponent<HealthbarController>().currentHealth =
                players[i].gameObject.GetComponent<HealthbarController>().maxHealth;

            players[i].SetPositionAndRotation(playerRespawnPositions[i], new Quaternion());
        }
        /*
        foreach(Transform enemy in enemies)
        {
            // reset enemy position and active status
            enemy.gameObject.SetActive(true);
            enemy.SetPositionAndRotation(new Vector3(0, 2, 0), new Quaternion());
        }*/
    }
}
