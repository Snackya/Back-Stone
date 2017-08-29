using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room19 : MonoBehaviour {
    [SerializeField]
    private Transform door19To20;
    [SerializeField]
    private Transform door18To19;

    private Transform fountain;
    private Transform weaponStands;
    private Transform answerPlates;

	// Use this for initialization
	void Start () {
        fountain = transform.FindChild("RevivalFountain");
        weaponStands = transform.FindChild("WeaponStands");
        answerPlates = transform.FindChild("AnswerPlates");
    }

    // Update is called once per frame
    public void closeDoors()
    {
        door18To19.GetChild(0).gameObject.SetActive(false);
        door18To19.GetChild(1).gameObject.SetActive(true);
    }

    public void openDoors()
    {
        door19To20.GetChild(0).gameObject.SetActive(true);
        door19To20.GetChild(1).gameObject.SetActive(false);

        door18To19.GetChild(0).gameObject.SetActive(true);
        door18To19.GetChild(1).gameObject.SetActive(false);
    }

    public void spawnFountain()
    {
        fountain.gameObject.SetActive(true);
        weaponStands.gameObject.SetActive(true);
    }
}
