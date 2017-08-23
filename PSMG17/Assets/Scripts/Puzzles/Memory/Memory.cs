using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour {

    private List<Transform> firstSet = new List<Transform>();
    private List<Transform> secondSet = new List<Transform>();

    [HideInInspector]
    public bool firstSetFlipable = true;
    [HideInInspector]
    public bool secondSetFlipable = true;

    private Room15 room15;

	private void Start ()
    {
        FillCardArrays();
        room15 = GetComponentInParent<Room15>();
    }

    private void FillCardArrays()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            firstSet.Add(transform.GetChild(0).GetChild(i).transform);
            secondSet.Add(transform.GetChild(1).GetChild(i).transform);
        }
    }

    private void Update ()
    {
        OnlyOneCardFlip();
        CheckForMatch();
        CheckIfMemoryComplete();
	}

    private void OnlyOneCardFlip()
    {
        for (int i = 0; i < firstSet.Count; i++)
        {
            if (firstSet[i].GetChild(0).gameObject.activeSelf)
            {
                firstSetFlipable = false;
            }
            if (secondSet[i].GetChild(0).gameObject.activeSelf)
            {
                secondSetFlipable = false;
            }
        }
    }

    private void CheckForMatch()
    {
        for (int i = 0; i < firstSet.Count; i++)
        {
            int flippedCards = 0;
            if (firstSet[i].GetChild(0).gameObject.activeSelf && secondSet[i].GetChild(0).gameObject.activeSelf)
            {
                Debug.Log("It's a match.");
                firstSet.RemoveAt(i);
                firstSetFlipable = true;

                secondSet.RemoveAt(i);
                secondSetFlipable = true;
            }
            for (int j = 0; j < firstSet.Count; j++)
            {
                if (firstSet[j].GetChild(0).gameObject.activeSelf) flippedCards++;
                else if (secondSet[j].GetChild(0).gameObject.activeSelf) flippedCards++;
            }
            if (flippedCards == 2)
            {
                StartCoroutine(FlipCardsToBack(i));
            }
        }
    }

    private IEnumerator FlipCardsToBack(int i)
    {
        yield return new WaitForSeconds(2f);
        firstSet[i].GetChild(0).gameObject.SetActive(false);
        firstSetFlipable = true;

        secondSet[i].GetChild(0).gameObject.SetActive(false);
        secondSetFlipable = true;
    }

    private void CheckIfMemoryComplete()
    {
        if (firstSet.Count == 0 && secondSet.Count == 0)
        {
            room15.memoryComplete = true;
        }
    }

    public void ResetPuzzle()
    {
        FillCardArrays();
        for (int i = 0; i < firstSet.Count; i++)
        {
            firstSet[i].GetChild(0).gameObject.SetActive(false);
            firstSetFlipable = true;

            secondSet[i].GetChild(0).gameObject.SetActive(false);
            secondSetFlipable = true;
        }
    }
}
