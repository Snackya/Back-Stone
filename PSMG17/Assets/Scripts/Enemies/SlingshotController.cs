using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public Transform[] targets;

    [HideInInspector]
    public Transform target;

    private Transform boulderSpawn;
    private GameObject boulder;
    private GameObject scream;
    private Animator animator;
    private Rigidbody2D enemy;


    // Use this for initialization

    private void Start()
    {
        animator = GetComponent<Animator>();
        boulderSpawn = GetComponentInChildren<Transform>();
    }
    private void OnEnable()
    {
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        SelectTarget();
        yield return new WaitForSeconds(3f);
        StartCoroutine(Attack());
    }

    private void SelectTarget()
    {
        int randomIndex = Random.Range(0, 2);
        target = targets[randomIndex];
        target = targets[1 - randomIndex];
    }

    private void SpawnBoulder()
    {
        GameObject newBoulder = Instantiate(boulder, boulderSpawn);
    }
}
