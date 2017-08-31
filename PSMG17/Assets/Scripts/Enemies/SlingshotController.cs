using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public Transform[] targets;
    public GameObject boulder;
    public Transform target;

    [SerializeField]
    private Transform boulderSpawn;
    private Animator animator;

    void OnEnable()
    {
        target = targets[0];
        animator = GetComponent<Animator>();
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        SelectTarget();
        animator.SetTrigger("attack"); 
        yield return new WaitForSeconds(0.2f);  //wait for the correct frame to spawn
        SpawnBoulder();
        yield return new WaitForSeconds(5f);
        StartCoroutine(Attack());
    }

    private void SelectTarget()
    {
        int randomIndex = Random.Range(0, 2);
        target = targets[randomIndex];
    }

    private void SpawnBoulder()
    {
        GameObject newBoulder = Instantiate(boulder, boulderSpawn);
    }
}
