using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room20 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private MusicManager musicManager;
    [SerializeField]
    private DialogManager diaMan;
    [SerializeField]
    private TextAsset deaconText;
    private Bounds bossbattleTrigger;
    private Transform backdoor;
    [SerializeField]
    private Slider deaconHealth;
    private Transform deacon;
    private Transform arrowAttack;
    private Transform pillars;
    [HideInInspector]
    public bool playersInside = false;
    private bool alreadyEnteredOnce = false;
    private Transform magicalBarrier;
    private Transform circles;
    private Transform princess;
    private bool bossAlreadyDied = false;

    private List<Transform> circlesList = new List<Transform>();
    private List<Transform> pillarsList = new List<Transform>();
    private List<Transform> boulderSpawnsList = new List<Transform>();

    // Use this for initialization
    void Start ()
    {
        bossbattleTrigger = transform.FindChild("BossBattleTrigger").GetComponent<BoxCollider2D>().bounds;
        backdoor = transform.FindChild("Backdoor");
        deacon = transform.FindChild("Deacon");
        arrowAttack = transform.FindChild("ArrowAttack");
        magicalBarrier = transform.FindChild("MagicalBarrier");
        pillars = magicalBarrier.FindChild("Pillars");
        circles = magicalBarrier.FindChild("Circles");
        princess = transform.FindChild("Princess");

        FillCirclesList();
        FillPillarsList();
        FillBoulderSpawnsList();
	}

    private void FillBoulderSpawnsList()
    {
        foreach (Transform boulderSpawnPosition in deacon.GetChild(0))
        {
            boulderSpawnsList.Add(boulderSpawnPosition);
        }
    }

    private void FillPillarsList()
    {
        foreach (Transform pillar in pillars)
        {
            pillarsList.Add(pillar);
        }
    }

    private void FillCirclesList()
    {
        foreach (Transform circle in circles)
        {
            circlesList.Add(circle);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        StartBossFight();
	}

    private void StartBossFight()
    {
        if (bossbattleTrigger.Contains(player1.position) && bossbattleTrigger.Contains(player2.position))
        {
            if (!playersInside)
            {
                if (!alreadyEnteredOnce)
                {
                    diaMan.StartDialog(deaconText, "The Deacon");
                }
                playersInside = true;
                CloseDoor();
                ActivateBoss();
                musicManager.StopBackGroundMusic();
                musicManager.PlayBossMusic2();
                StartCoroutine(CheckForDeadEnemy());
            }
        }
    }
    
    private IEnumerator CheckForDeadEnemy()
    {
        if (playersInside && deacon.GetComponent<EnemyHealth>().health.CurrentVal == 0)
        {
            princess.gameObject.SetActive(true);
            DeactivateBoss();
            magicalBarrier.gameObject.SetActive(false);
            if (!bossAlreadyDied)
            {
                musicManager.StopBossMusic2();
                musicManager.StopBackGroundMusic();
                musicManager.PlayEndingMusic();
            }
            bossAlreadyDied = true;
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(CheckForDeadEnemy());
        }
    }

    private void RotateCircles()
    {
        foreach (Transform circle in circles)
        {
            circle.GetComponent<CircleScript>().RotateCircles();
        }
    }

    private void StopCircleRotation()
    {
        foreach (Transform circle in circles)
        {
            circle.GetComponent<CircleScript>().StopAllCoroutines();
        }
    }

    private void ActivateBoss()
    {
        RotateCircles();
        deacon.gameObject.SetActive(true);
        arrowAttack.gameObject.SetActive(true);
        pillars.gameObject.SetActive(true);
        deaconHealth.GetComponentInChildren<Text>().text = "The Deacon";
        deaconHealth.gameObject.SetActive(true);
    }

    private void DeactivateBoss()
    {
        ResetPillars();
        ResetCircles();
        StopCircleRotation();
        ResetBoulders();
        deacon.GetComponent<EnemyHealth>().health.CurrentVal = 
            deacon.GetComponent<EnemyHealth>().health.MaxVal;
        deacon.gameObject.SetActive(false);
        ResetArrows();
        arrowAttack.gameObject.SetActive(false);
        pillars.gameObject.SetActive(false);
        deaconHealth.gameObject.SetActive(false);
        
    }

    private void ResetArrows()
    {
        foreach (Transform arrow in arrowAttack)
        {
            Destroy(arrow.gameObject);
        }
    }

    private void ResetBoulders()
    {
        foreach (Transform spawnPositions in boulderSpawnsList)
        {
            foreach (Transform spawnPosition in spawnPositions)
            {
                foreach (Transform boulder in spawnPosition)
                {
                    Destroy(boulder.gameObject);
                }
            }
        }
    }

    private void ResetPillars()
    {
        foreach (Transform pillar in pillarsList)
        {
            pillar.GetComponent<EnemyHealth>().health.CurrentVal =
                pillar.GetComponent<EnemyHealth>().health.MaxVal;
            pillar.GetChild(0).gameObject.SetActive(true);
            pillar.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void ResetCircles()
    {
        foreach (Transform circle in circlesList)
        {
            circle.gameObject.SetActive(true);
        }
    }

    private void CloseDoor()
    {
        backdoor.GetChild(1).gameObject.SetActive(true);
        backdoor.GetChild(0).gameObject.SetActive(false);
    }

    private void OpenDoor()
    {
        backdoor.GetChild(0).gameObject.SetActive(true);
        backdoor.GetChild(1).gameObject.SetActive(false);
    }

    public void ResetRoom()
    {
        playersInside = false;
        OpenDoor();
        DeactivateBoss();
        musicManager.StopBossMusic2();
        musicManager.PlayBackgroundMusic();
    }
}
