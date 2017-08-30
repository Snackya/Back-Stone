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
    private Bounds bossbattleTrigger;
    private Transform backdoor;
    [SerializeField]
    private Slider deaconHealth;
    private Transform deacon;
    private Transform arrowAttack;
    private Transform pillars;
    private bool playersInside = false;

    // Use this for initialization
    void Start ()
    {
        bossbattleTrigger = transform.FindChild("BossBattleTrigger").GetComponent<BoxCollider2D>().bounds;
        backdoor = transform.FindChild("Backdoor");
        deacon = transform.FindChild("Deacon");
        arrowAttack = transform.FindChild("ArrowAttack");
        pillars = transform.FindChild("MagicalBarrier").FindChild("Pillars");
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
                playersInside = true;
                CloseDoor();
                ActivateBoss();
                musicManager.StopBackGroundMusic();
                musicManager.PlayBossMusic2();
            }
        }
    }

    private void ActivateBoss()
    {
        deacon.gameObject.SetActive(true);
        arrowAttack.gameObject.SetActive(true);
        pillars.gameObject.SetActive(true);
        deaconHealth.GetComponentInChildren<Text>().text = "The Deacon";
        deaconHealth.gameObject.SetActive(true);
    }

    private void DeactivateBoss()
    {
        deacon.GetComponent<EnemyHealth>().health.CurrentVal = 
            deacon.GetComponent<EnemyHealth>().health.MaxVal;
        deacon.gameObject.SetActive(false);
        arrowAttack.gameObject.SetActive(false);
        pillars.gameObject.SetActive(false);
        deaconHealth.gameObject.SetActive(false);
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
