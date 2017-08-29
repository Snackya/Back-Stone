using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room15Sign : MonoBehaviour {
    [SerializeField]
    private DialogManager diaMan;
    [SerializeField]
    private TextAsset signText;
    private BoxCollider2D col;
    private bool signRead = false;
	
    // Use this for initialization
	void Start () {
        col = GetComponent<BoxCollider2D>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            signRead = false;
        }
    }

	void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonUp("Action1") && !signRead)
        {
            //prevent trigger Action1 from overwriting dialog manager Action1
            signRead = true;
            StartCoroutine(PlayDialog());
        }
    }

    //Coroutine prevents the first textbox from being displayed instantly
    IEnumerator PlayDialog()
    {
        yield return new WaitForSeconds(0.1f);
        diaMan.StartDialog(signText, "Wooden Sign");
    }
}
