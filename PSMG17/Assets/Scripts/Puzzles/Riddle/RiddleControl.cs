using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleControl : MonoBehaviour {
    private static string speakerName = "Lord Al Coholic";

    public DialogManager diaMan;
    public TextAsset riddle1;
    public TextAsset riddle2;
    public TextAsset riddle3;

    private BoxCollider2D dialogTrigger;
    private Transform answerPlates;
    private PressurePlate answerA;
    private PressurePlate answerB;
    private PressurePlate answerC;
    private Transform teleporter;

    private string correctAnswer;
    private bool riddle1Correct;
    private bool riddle2Correct;
    private bool riddle3Correct;
    private bool isKillable;

    // Use this for initialization
    void Start () {
        dialogTrigger = GetComponent<BoxCollider2D>();
        answerPlates = transform.GetChild(0);
        answerA = transform.GetChild(0).GetChild(0).GetComponent<PressurePlate>();
        answerB = transform.GetChild(0).GetChild(1).GetComponent<PressurePlate>();
        answerC = transform.GetChild(0).GetChild(2).GetComponent<PressurePlate>();
        isKillable = false;
        teleporter = transform.parent.GetChild(1);
    }

    private void Update()
    {
        CheckForAnswerInput();
    }

    void OnDisable()
    {
        teleporter.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //after the first riddle, make the riddleGuy killable. if killed disable him, but leave plates
        if(collision.gameObject.tag == "PlayerWeapon" && isKillable)
        {
            answerPlates.SetParent(transform.parent, true);
            transform.gameObject.SetActive(false);
        }
    }

    public void StartRiddles()
    {
        //ask questions one after antoher
        riddle1Correct = false;
        riddle2Correct = false;
        riddle3Correct = false;

        diaMan.StartDialog(riddle1, speakerName);
        correctAnswer = "B";
        StartCoroutine(CheckForAnswerInput());
    }

    IEnumerator CheckForAnswerInput()
    {
        if(answerA.isActive || answerB.isActive || answerC.isActive)
        {
            if (AnswerIsCorrect())
            {
                //check for current position in riddle queue
                if (riddle3Correct)
                {
                    isKillable = true;
                    yield return new WaitForSeconds(1f);    //debug delay
                    StartRiddles();
                }
                else if (riddle2Correct)
                {
                    correctAnswer = "B";
                    riddle3Correct = true;
                    diaMan.StartDialog(riddle3, speakerName);
                }
                else if (riddle1Correct)
                {
                    correctAnswer = "A";
                    riddle2Correct = true;
                    diaMan.StartDialog(riddle2, speakerName);
                }
                else
                {
                    riddle1Correct = true;
                }
            }
            /**
             * 
             * 
             * 
             * 
             * **/
            else
            {
                Debug.Log("wrong answer");
            }

        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckForAnswerInput());
    }

    //bool equals correctness of answer
    bool AnswerIsCorrect()
    {
        if (answerA.isActive && correctAnswer != "A"
            || answerB.isActive && correctAnswer != "B"
            || answerC.isActive && correctAnswer != "C")
        {
            return false;
        }
        else return true;
    }
}
