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

    private SpikePlate spikesA;
    private SpikePlate spikesB;
    private SpikePlate spikesC;

    private Room19 roomManager;

    private string correctAnswer;
    private bool riddle1Correct;
    private bool riddle2Correct;
    private bool riddle3Correct;
    private bool alreadyPunished = false;
    private bool plateAlreadyPressed = false;
    private bool isKillable;

    // Use this for initialization
    void Start () {
        dialogTrigger = GetComponent<BoxCollider2D>();
        roomManager = transform.parent.GetComponent<Room19>();
        answerPlates = transform.GetChild(0);
        isKillable = false;

        answerA = transform.GetChild(0).GetChild(0).GetComponent<PressurePlate>();
        answerB = transform.GetChild(0).GetChild(1).GetComponent<PressurePlate>();
        answerC = transform.GetChild(0).GetChild(2).GetComponent<PressurePlate>();

        spikesA = transform.GetChild(2).FindChild("SpikeTrapA").GetComponent<SpikePlate>();
        spikesB = transform.GetChild(2).FindChild("SpikeTrapB").GetComponent<SpikePlate>();
        spikesC = transform.GetChild(2).FindChild("SpikeTrapC").GetComponent<SpikePlate>();
    }

    void OnDisable()
    {
        roomManager.openDoors();
        roomManager.spawnFountain();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //after the first riddle, make the riddleGuy killable. if killed disable him, but leave plates
        if(collision.gameObject.tag == "PlayerWeapon" && isKillable)
        {
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
        answerPlates.gameObject.SetActive(true);
        correctAnswer = "B";
        StartCoroutine(CheckForAnswerInput());
    }

    IEnumerator CheckForAnswerInput()
    {
        if(answerA.isActive || answerB.isActive || answerC.isActive)
        {
            if (AnswerIsCorrect())
            {
                //prevent false negative answers
                
                //check for current position in riddle queue
                if (riddle3Correct)
                {
                    isKillable = true;
                    yield return new WaitForSeconds(2f);    //delay for new start
                    StartRiddles();                         //provoke player to kill Lord Al Coholic by starting anew
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
            else if(!AnswerIsCorrect() && !plateAlreadyPressed)
            {
                //use bool to only call Punish once in the coroutine         
                StartCoroutine(PunishWrongAnswer());
                alreadyPunished = true;
            }
            plateAlreadyPressed = true;
        }
        //reset Punish bool
        else
        {
            alreadyPunished = false;
            plateAlreadyPressed = false;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckForAnswerInput());
    }

    //bool equals correctness of answer
    bool AnswerIsCorrect()
    {
        if (answerA.isActive && correctAnswer != "A" || answerB.isActive && correctAnswer != "B" || answerC.isActive && correctAnswer != "C" && !plateAlreadyPressed)
        {
            return false;
        }     
        else return true;
    }

    IEnumerator PunishWrongAnswer()
    {
        if (!alreadyPunished)
        {
            if (answerA.isActive)
            {
                spikesA.ActivateSpikes();
            }
            else if (answerB.isActive)
            {
                spikesB.ActivateSpikes();
            }
            else if (answerC.isActive)
            {
                spikesC.ActivateSpikes();
            }                
        }

        yield return new WaitForSeconds(2f);
        ResetSpikes();
    }

    void ResetSpikes()
    {
        spikesA.DeactivateSpikes();
        spikesB.DeactivateSpikes();
        spikesC.DeactivateSpikes();
    }
}
