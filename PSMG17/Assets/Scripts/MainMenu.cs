using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private DialogManager diaMan;
    [SerializeField]
    private TextAsset introText;
    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;

    void OnEnable()
    {
        player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void StartGame()
    {
        player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        diaMan.StartDialog(introText, "A voice from somewhere");
        transform.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
