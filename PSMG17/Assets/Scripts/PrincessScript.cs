using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessScript : MonoBehaviour {
    [SerializeField]
    private DialogManager diaMan;
    [SerializeField]
    private TextAsset princessText;
    [SerializeField]
    private GameObject credits;
    private Animator animator;
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        animator = credits.GetComponent<Animator>();
        diaMan.StartDialog(princessText, "Princess Poppy Honey Rosie");
        StartCoroutine(WaitForDialogEnd());
    }

    private IEnumerator WaitForDialogEnd()
    {
        if (!diaMan.isRunning)
        {
            //end the game; GG
            credits.SetActive(true);
            animator.enabled = true;
            transform.gameObject.SetActive(false);
            //play some sicc credits music
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(WaitForDialogEnd());
        }
    }
}
