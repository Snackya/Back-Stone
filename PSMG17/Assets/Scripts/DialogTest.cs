using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour {
    // Use this for initialization
    public TextAsset text;
    public DialogManager diaCon;
	void Start () {
        diaCon = FindObjectOfType<DialogManager>();
        diaCon.StartDialog(text, "Player 1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
