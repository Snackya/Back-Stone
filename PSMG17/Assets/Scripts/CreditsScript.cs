using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour {
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(WaitForFade());
    }
    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(3f);
        player1.SetActive(false);
        player2.SetActive(false);
    }
}
