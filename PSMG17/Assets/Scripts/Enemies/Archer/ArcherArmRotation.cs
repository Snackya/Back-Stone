using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArmRotation : MonoBehaviour {

    [HideInInspector]
    public int rotationOffset = -30;   //210
    private Transform target;

    void Awake()
    {
        target = GetComponentInParent<EnemyAI>().target;
    }

    // Update is called once per frame
    void Update()
    {
        target = GetComponentInParent<EnemyAI>().target;
        // for testing only
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // use this
        Vector3 difference = target.position - transform.position;
        difference.Normalize();     // normalizing the vector. meaning that all the sum of the vector will be equal to 1.

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;   // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
}
