using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public Text textField;
    public GameObject textBox;
    public Text speakerName;
    public string[] textLines;

    public PlayerController player1Control;
    public PlayerController player2Control;

    private int indexCurrentLine;
    private int indexLastLine;
    private bool isTyping;
    private bool isEndOfDialog;
    private bool cancelTyping;
    private float secondsBetweenCharacters = 0.05f;

    void Update()
    {
        if (Input.GetButtonUp("Action1"))
        {
            if (!isTyping)
            {
                indexCurrentLine += 1;
                if(indexCurrentLine >= indexLastLine)
                {
                    textBox.SetActive(false);
                    //re-enable movement after text finishes
                    player1Control.canMove = true;
                    player2Control.canMove = true;
                }
                else
                {
                    StartCoroutine(DisplayText(textLines[indexCurrentLine]));
                }
            }
            else if(isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    public void StartDialog (TextAsset dialog, string speaker)
    {
        //disable player movement during dialog
        player1Control.canMove = false;
        player2Control.canMove = false;

        speakerName.text = speaker;
        textLines = dialog.text.Split('\n');
        isTyping = true;
        cancelTyping = false;
        indexCurrentLine = 0;
        indexLastLine = textLines.Length;

        textBox.SetActive(true);
        StartCoroutine(DisplayText(textLines[indexCurrentLine]));
    }

    //source: https://github.com/SABentley/Zelda-Dialogue/blob/master/Assets/Scripts/Dialogue.cs
    //shoot text letter by letter until interupted by action key in Update()
    IEnumerator DisplayText(string stringToDisplay)
    {
        int currentCharacterIndex = 0;
        int stringLength = stringToDisplay.Length;
        isTyping = true;
        cancelTyping = false;
        //HideIcons();
        textField.text = "";
        while (currentCharacterIndex < stringLength && !cancelTyping && isTyping)
        {
            textField.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex += 1;
            yield return new WaitForSeconds(secondsBetweenCharacters);
        }
        textField.text = stringToDisplay;
        //ShowIcon();
        isTyping = false;
        cancelTyping = false;
    }
}
