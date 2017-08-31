using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(RollTheFuckingCredits());
    }
    private IEnumerator RollTheFuckingCredits()
    {
        yield return new WaitForSeconds(3f);
        player1.SetActive(false);
        player2.SetActive(false);
        yield return new WaitForSeconds(60f);   //quit game with standard keys 3s after credits ran through
        StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame()
    {
        if (Input.GetButtonDown("Action1") || Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(QuitGame());
    }
}

