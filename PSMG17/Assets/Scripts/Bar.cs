using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    // [SerializeField]
    private float fillAmount;

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

	// Use this for initialization
	void Start () {
        Value = 2;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateBar();
	}

    private void UpdateBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;
        }
    }

    private float Map(float value, float inMax)
    {

        return (value / inMax);
    }
}
