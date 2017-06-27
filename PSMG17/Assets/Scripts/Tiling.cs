using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public int offsetX = 2;                 // the offset -> no weird errors
    public int offsetY = 2;

    // used for checking if there is stuff to be instantiated
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    public bool hasATopBuddy = false;
    public bool hasABottomBuddy = false;

    public bool reverseScale = false;       // used if the object is not tilable

    private float spriteWidth = 0f;         // the width of the element
    private float spriteHeight = 0f;        // the height of the element
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
        spriteHeight = sRenderer.sprite.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        // does it still need buddies?
        if (hasALeftBuddy == false || hasARightBuddy == false || hasABottomBuddy == false || hasATopBuddy == false)
        {
            // calculate the cameras extend (half the width) of what the camera can see in world coordinates
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
            // calculate the cameras extend (half of the height) of what the camera can see in world coordinates
            float camVerticalExtend = cam.orthographicSize * Screen.height / Screen.width;

            // calculate the x position, where the camera can see the edge of the sprite
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            // calculate the y position, where the camera can see the edge of the sprite
            float edgeVisiblePositionTop = (myTransform.position.y + spriteHeight / 2) - camVerticalExtend;
            float edgeVisiblePositionBottom = (myTransform.position.y - spriteHeight / 2) + camVerticalExtend;

            // checking if you can see the edge od the element and calling MakeNewBuddy if you can
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeLeftOrRightBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeLeftOrRightBuddy(-1);
                hasALeftBuddy = true;
            }
            else if (cam.transform.position.y >= edgeVisiblePositionTop - offsetY && hasATopBuddy == false)
            {
                MakeTopOrBottomBuddy(1);
                hasATopBuddy = true;
            }
            else if (cam.transform.position.y <= edgeVisiblePositionBottom + offsetY && hasABottomBuddy == false)
            {
                MakeTopOrBottomBuddy(-1);
                hasABottomBuddy = true;
            }
        }
    }

    void MakeTopOrBottomBuddy (int topOrBottom)
    {
        Vector3 newPosition = new Vector3(myTransform.position.x, myTransform.position.y + spriteHeight * topOrBottom, myTransform.position.z);
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x, newBuddy.localScale.y * -1, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if (topOrBottom > 0)
        {
            newBuddy.GetComponent<Tiling>().hasABottomBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasATopBuddy = true;
        }
    }

    // creates a buddy on the side required
    void MakeLeftOrRightBuddy (int rightOrLeft)
    {
        // calculating the new position for the new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        // instantiating a new buddy and storing him in a variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        // if not tilable,  the x size of the object is reversed, to get rid of ugly scenes
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
