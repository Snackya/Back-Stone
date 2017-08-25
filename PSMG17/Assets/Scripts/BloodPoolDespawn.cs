using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPoolDespawn : MonoBehaviour
{
    private float alpha; 
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        alpha = 1f;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        alpha -= 0.1f;
        sr.color = new Color(1f, 1f, 1f, alpha);
        yield return new WaitForSeconds(0.1f);
        if(sr.color.a == 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(FadeOut());
    }
}