using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WandStand : MonoBehaviour {

    private int newMaxHealth = 100;
    [SerializeField]
    private Transform[] skillIcons;
    [SerializeField]
    private Sprite player1Icon;
    [SerializeField]
    private Sprite player2Icon;
    [SerializeField]
    private AudioSource weaponSwitchSound;
    [SerializeField]
    private SwipeAttack swipeAttackPlayer1;
    [SerializeField]
    private SwipeAttack swipeAttackPlayer2;
    [SerializeField]
    private Animator animatorPlayer1;
    [SerializeField]
    private Animator animatorPlayer2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int playerNumber = collision.gameObject.GetComponent<PlayerController>().playerNumber;
            Animator animator = GetComponentInParent<Animator>();

            if (playerNumber == 1)
            {
                animatorPlayer1.ResetTrigger("swipeTrigger");
                swipeAttackPlayer1.cooldown.CurrentVal = swipeAttackPlayer1.cooldown.MaxVal;
            }
            else
            {
                animatorPlayer2.ResetTrigger("swipeTrigger");
                swipeAttackPlayer2.cooldown.CurrentVal = swipeAttackPlayer2.cooldown.MaxVal;
            }

            weaponSwitchSound.Play();

            collision.gameObject.GetComponent<PlayerController>().swordEquipped = false;
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
