using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    private float fillAmount;

    private float lerpSpeed = 6;

    [SerializeField]
    private Image content;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            fillAmount = Map(value, MaxValue);
        }
    }

	void Start () {
	}
	
	void Update () {
        UpdateBar();
	}

    private void UpdateBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
    }

    private float Map(float value, float inMax)
    {

        return (value / inMax);
    }
}
