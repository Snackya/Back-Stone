using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPressurePlates : MonoBehaviour {

    [SerializeField]
    private Transform[] hubs;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < hubs.Length; i++)
        {
            Debug.Log(hubs[i]);
            if (i == 0)
            {
                hubs[i].Rotate(new Vector3(0, 0, 90));
            }
            else
            {
                hubs[i].Rotate(new Vector3(0, 0, -90));
            }
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < hubs.Length; i++)
        {
            if (i == 0)
            {
                hubs[i].rotation = Quaternion.Lerp(hubs[i].rotation,
                    Quaternion.Euler(new Vector3(hubs[i].rotation.x,
                    hubs[i].rotation.y,
                    hubs[i].rotation.z + 90)),
                    1);
            }
            else
            {
                hubs[i].rotation = Quaternion.Lerp(hubs[i].rotation,
                    Quaternion.Euler(new Vector3(hubs[i].rotation.x,
                    hubs[i].rotation.y,
                    hubs[i].rotation.z - 90)),
                    1);
            }

        }
    }*/
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < hubs.Length; i++)
        {
            Vector3 currentAngle = hubs[i].eulerAngles;

            if (i == 0)
            {
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, currentAngle.x, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.y, currentAngle.y, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.z, currentAngle.z + 90, Time.deltaTime * 50)
                    );

            }
            else
            {
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, currentAngle.x, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.y, currentAngle.y, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.z, currentAngle.z - 90, Time.deltaTime * 50)
                    );
            }

            hubs[i].eulerAngles = currentAngle;
        }
    }*/

}
