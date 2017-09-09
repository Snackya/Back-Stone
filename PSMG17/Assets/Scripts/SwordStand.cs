using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordStand : MonoBehaviour {

    private int newMaxHealth = 120;
    [SerializeField]
    private Transform[] skillIcons;
    [SerializeField]
    private Sprite player1Icon;
    [SerializeField]
    private Sprite player2Icon;
    [SerializeField]
    private AudioSource weaponSwitchSound;
    [SerializeField]
    private WandHeal wandHealPlayer1;
    [SerializeField]
    private WandHeal wandHealPlayer2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int playerNumber = collision.gameObject.GetComponent<PlayerController>().playerNumber;

            if (playerNumber == 1) wandHealPlayer1.cooldown.CurrentVal = wandHealPlayer1.cooldown.MaxVal;
            else wandHealPlayer2.cooldown.CurrentVal = wandHealPlayer2.cooldown.MaxVal;
            
            weaponSwitchSound.Play();

            collision.gameObject.GetComponent<PlayerController>().swordEquipped = true;
            collision.gameObject.GetComponent<HealthbarController>().maxHealth = newMaxHealth;
            collision.gameObject.GetComponent<HealthbarController>().currentHealth =
                collision.gameObject.GetComponent<HealthbarController>().maxHealth;
            ChangeSkillIcon(playerNumber);
        }
    }

    private void ChangeSkillIcon(int pNum)
    {
        if (pNum == 1)
        {
            skillIcons[0].GetComponent<Image>().sprite = player1Icon;
            skillIcons[0].GetChild(0).GetComponent<Image>().sprite = player1Icon;
        }
        else if (pNum == 2)
        {
            skillIcons[1].GetComponent<Image>().sprite = player2Icon;
            skillIcons[1].GetChild(0).GetComponent<Image>().sprite = player2Icon;
        }
    }
}
