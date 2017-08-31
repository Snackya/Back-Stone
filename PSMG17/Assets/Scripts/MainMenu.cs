using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    private GameObject startGameButton;
    private GameObject controlsButton;
    private GameObject quitGameButton;

    [SerializeField]
    private Font unselectedFont;
    [SerializeField]
    private Font selectedFont;
    [SerializeField]
    private DialogManager diaMan;
    [SerializeField]
    private TextAsset introText;
    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;

    private bool startSelected;
    private bool controlsSelected;
    private bool quitSelected;
    private bool navigationRunning = false;

    void Start () {
        startGameButton = transform.GetChild(2).gameObject;
        controlsButton = transform.GetChild(3).gameObject;
        quitGameButton = transform.GetChild(4).gameObject;

        startSelected = true;
        controlsSelected = false;
        quitSelected = false;

    }

    void OnEnable()
    {
        player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(Navigate());
    }

    void Update () {
        NavigateWithKeys();
	}

    private void NavigateWithKeys()
    {
        HighlightButtons();
        CheckForButtonSelect();
    }

    private IEnumerator Navigate()
    {
        float verticalInput = Input.GetAxis("Vertical1");
        float gamepadVerticalInput = Input.GetAxis("GamepadVertical1");
        if (verticalInput == 1 || gamepadVerticalInput == 1)
        {
            navigationRunning = true;
            if (!quitSelected && controlsSelected && !startSelected)
            {
                controlsSelected = false;
                startSelected = true;
            }
            if (quitSelected && !controlsSelected && !startSelected)
            {
                controlsSelected = true;
                quitSelected = false;
            }
        }
        if (verticalInput == -1 || gamepadVerticalInput == -1)
        {
            navigationRunning = true;
            if (!quitSelected && controlsSelected && !startSelected)
            {
                controlsSelected = false;
                quitSelected = true;
            }
            if (!quitSelected && !controlsSelected && startSelected)
            {
                controlsSelected = true;
                startSelected = false;
            }
        }
        yield return new WaitForSeconds(0.08f);     //instant skip workaround
        StartCoroutine(Navigate());
    }

    private void HighlightButtons()
    {
        if (quitSelected)
        {
            quitGameButton.GetComponentInChildren<Text>().font = selectedFont;
        }
        else
        {
            quitGameButton.GetComponentInChildren<Text>().font = unselectedFont;
        }

        if (controlsSelected)
        {
            controlsButton.GetComponentInChildren<Text>().font = selectedFont;
        }
        else
        {
            controlsButton.GetComponentInChildren<Text>().font = unselectedFont;
        }

        if (startSelected)
        {
            startGameButton.GetComponentInChildren<Text>().font = selectedFont;
        }
        else
        {
            startGameButton.GetComponentInChildren<Text>().font = unselectedFont;
        }
    }

    private void CheckForButtonSelect()
    {
        if (Input.GetButtonDown("Action1") || Input.GetButtonDown("Submit"))
        {
            if (startSelected && !quitSelected && !quitSelected)
            {
                StartGame();
            }
            else if (!startSelected && controlsSelected && !quitSelected)
            {
                //ShowControls();
            }
            else if(!startSelected && !controlsSelected && quitSelected)
            {
                Application.Quit();
            }
        }
    }

    private void StartGame()
    {
        player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        diaMan.StartDialog(introText, "A voice from somewhere");
        transform.gameObject.SetActive(false);
    }
}
